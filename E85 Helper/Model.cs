using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace E85_Helper
{
    [XmlRoot]
    public class Model
    {
        [XmlElement]
        public Vehicle Car { get; set; }
        [XmlArray]
        public List<FuelType> Fuels { get; set; }
    }
}
