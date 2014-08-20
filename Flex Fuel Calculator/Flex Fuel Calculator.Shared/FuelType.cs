using System;
using System.Collections.Generic;
using System.Text;

namespace Flex_Fuel_Calculator
{
    public class FuelType
    {
        public string Name { get; set; }
        public double PricePerVolumeUnit { get; set; }
        public double GallonOfGasEquivalent { get; set; }
        public double KgOfCarbonEmissionsPerVolumeUnit { get; set; }
        public double VehicleSpecificDistanceUnitsPerVolumeUnit { get; set; }
        public double PricePerDistanceUnit { get { return PricePerVolumeUnit / VehicleSpecificDistanceUnitsPerVolumeUnit; } }
        public double KiloGramsOfCarbonEmissionsPerDistanceUnit { get { return KgOfCarbonEmissionsPerVolumeUnit * GallonOfGasEquivalent / VehicleSpecificDistanceUnitsPerVolumeUnit; } }
    }
}
