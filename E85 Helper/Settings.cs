using System.Collections.Generic;
using System.Xml.Serialization;
using System.Globalization;

namespace E85_Helper
{
    [XmlRoot]
    public class Settings
    {
        [XmlElement]
        public List<Vehicle> Vehicles { get; set; }
        [XmlElement]
        public string VolumeUnit { get; set; }
        [XmlElement]
        public string DistanceUnit { get; set; }
        [XmlElement]
        public List<FuelType> Blends { get; set; }
    }
}
