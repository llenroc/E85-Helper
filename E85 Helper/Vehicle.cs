using System.Collections.Generic;
using System.Xml.Serialization;

namespace E85_Helper
{
    public class Vehicle
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public int Odometer { get; set; }
        [XmlAttribute]
        public float TankSize { get; set; }
        [XmlAttribute]
        public float Ethanol { get; set; }
        [XmlAttribute]
        public float Gasoline { get; set; }
        [XmlAttribute]
        public float UnknownFuel { get; set; }
        [XmlElement]
        public List<FuelUsageDataPoint> MPGDataPoints { get; set; }

        public Vehicle()
        {
            MPGDataPoints = new List<FuelUsageDataPoint>();
        }

        /// <summary>
        /// Creates a new MPG point and updates the tank contents. 
        /// </summary>
        /// <param name="blend">What FuelType was added</param>
        /// <param name="volume">How much fuel in local units was added</param>
        /// <param name="newOdometer">The current mileage on the car. Used to calculate MPG.</param>
        public void FillUpTank(FuelType blend, float volume, int distance)
        {
            MPGDataPoints.Add(new FuelUsageDataPoint() { PercentEthanol = (this.Ethanol/ this.TankSize), Efficiency = ( (distance)/volume)});
            
            var oldVolume = TankSize - volume;
            Ethanol = Ethanol * (oldVolume / TankSize);
            Gasoline = Gasoline * (oldVolume / TankSize);
            UnknownFuel = UnknownFuel * (oldVolume / TankSize);

            Ethanol += blend.PercentEthanol * volume;
            Gasoline += (1 - blend.PercentEthanol) * volume;
        }
    }


    public class FuelUsageDataPoint
    {
        [XmlAttribute]
        public float PercentEthanol { get; set; }
        [XmlAttribute]
        public float Efficiency { get; set; }
    }
}
