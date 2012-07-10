using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using System.IO.IsolatedStorage;


namespace E85_Helper
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Fuels = new ObservableCollection<FuelType>();
            Car = new Vehicle();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<FuelType> Fuels { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Model));
                FileStream fs = IsolatedStorageFile.GetUserStoreForApplication().OpenFile("flexfuel.xml", FileMode.Open);

                var model = (Model)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {
                Fuels.Add(new FuelType() { Name = "E85", PercentEthanol = .85f, Price = 2.67f });
                Fuels.Add(new FuelType() { Name = "E10", PercentEthanol = .10f, Price = 3.46f });
                Fuels.Add(new FuelType() { Name = "Gasoline", PercentEthanol = 0, Price = 3.86f });

                Car.Odometer = 0;
                Car.TankSize = 10;
                Car.UnknownFuel = 1;
                Car.Ethanol = 0;
                Car.Gasoline = 0;
            }



            //Car.Name = "Boris";
            //Car.FillUpTank(new FuelType() { PercentEthanol = .10f }, 10, 290);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .85f }, 8.9f, 290);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .10f }, 5.7f, 230);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .85f }, 9.4f, 350);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .10f }, 9.7f, 220);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .10f }, 9.6f, 290);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .85f }, 9.4f, 290);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .85f }, 9.1f, 180);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .85f }, 9.3f, 180);
            //Car.FillUpTank(new FuelType() { PercentEthanol = .10f }, 9.4f, 180);

            // Sample data; replace with real data
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Vehicle Car { get; set; }
        public float EstRemainingFuel { get { return _erf; } set { _erf = value; RecalcAll(); } }

        public void RecalcAll()
        {
            foreach (var fuel in Fuels)
                fuel.Recalc();
        }
        private float _erf;

        public FuelType SelectedFuel { get; set; }
    }
}