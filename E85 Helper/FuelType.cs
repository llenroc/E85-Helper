using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Media;

namespace E85_Helper
{
    public class FuelType : INotifyPropertyChanged
    {
        /// <summary>
        /// Human friendly display name
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// Percentage of ethanol in the fuel. Represented as a float in range 0..1
        /// </summary>
        [XmlAttribute]
        public float PercentEthanol { get; set; }
        /// <summary>
        /// Price last seen in local currency.
        /// </summary>
        [XmlAttribute]
        public float Price { get { return _price; } set { _price = value; App.ViewModel.RecalcAll(); } }
        private float _price;

        [XmlIgnore]
        public float Score
        {
            get
            {
                var oldblend = (App.ViewModel.EstRemainingFuel * App.ViewModel.Car.Ethanol);
                var newblend = (App.ViewModel.Car.TankSize - App.ViewModel.EstRemainingFuel) * PercentEthanol;
                var predictedPercentEthanol = (oldblend + newblend) / App.ViewModel.Car.TankSize;
                return (float)Math.Round((_price ) / (new FuelType() { PercentEthanol = predictedPercentEthanol }.FuelYield(App.ViewModel.Car)), 2);
            }
        }

        public Brush Color
        {
            get
            {
                if (App.ViewModel.Fuels.Select(f => f.Score).Min() == Score)
                    return (SolidColorBrush)App.Current.Resources["PhoneAccentBrush"];
                else
                    return
                        (SolidColorBrush)App.Current.Resources["PhoneForegroundBrush"];
            }
        }

        public float MPG
        {
            get { return FuelYield(App.ViewModel.Car); }
        }

        public float FuelYield(Vehicle vehicle)
        {
            var PercentEthanols = new List<float>();
            var MPGs = new List<float>();

            foreach (var dp in vehicle.MPGDataPoints)
            {
                PercentEthanols.Add(dp.PercentEthanol);
                MPGs.Add(dp.Efficiency);
            }
            float r2;
            float baseline;
            float slope;
            LinearRegression(PercentEthanols.ToArray(), MPGs.ToArray(), 0, MPGs.Count, out r2, out baseline, out slope);

            var estMPG = PercentEthanol * slope + baseline;
            return estMPG;
        }
        /// <summary>
        /// Fits a line to a collection of (x,y) points.
        /// </summary>
        /// <param name="xVals">The x-axis values.</param>
        /// <param name="yVals">The y-axis values.</param>
        /// <param name="inclusiveStart">The inclusive inclusiveStart index.</param>
        /// <param name="exclusiveEnd">The exclusive exclusiveEnd index.</param>
        /// <param name="rsquared">The r^2 value of the line.</param>
        /// <param name="yintercept">The y-intercept value of the line (i.e. y = ax + b, yintercept is b).</param>
        /// <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
        public static void LinearRegression(float[] xVals, float[] yVals,
                                            int inclusiveStart, int exclusiveEnd,
                                            out float rsquared, out float yintercept,
                                            out float slope)
        {
            //Debug.Assert(xVals.Length == yVals.Length);
            float sumOfX = 0;
            float sumOfY = 0;
            float sumOfXSq = 0;
            float sumOfYSq = 0;
            float ssX = 0;
            float ssY = 0;
            float sumCodeviates = 0;
            float sCo = 0;
            float count = exclusiveEnd - inclusiveStart;

            for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
            {
                float x = xVals[ctr];
                float y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            float RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            float RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            float meanX = sumOfX / count;
            float meanY = sumOfY / count;
            float dblR = RNumerator / (float)Math.Sqrt(RDenom);
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void Recalc()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""));
        }
    }
}
