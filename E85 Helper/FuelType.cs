using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace E85_Helper
{
    public class FuelType
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
        public float LastKnownPrice { get; set; }

        public float FuelYield(Vehicle vehicle)
        {

            return .1f;
        }
    }
}
