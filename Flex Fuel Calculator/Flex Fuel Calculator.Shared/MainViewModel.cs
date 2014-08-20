using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Flex_Fuel_Calculator
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Fuels = new ObservableCollection<FuelType>()
            {
                new FuelType{ Name = "Regular Gasoline - No Ethanol", GallonOfGasEquivalent = 1.0f, PricePerVolumeUnit = 3.75f, VehicleSpecificDistanceUnitsPerVolumeUnit = 35, KgOfCarbonEmissionsPerVolumeUnit = 8.887},
                new FuelType{ Name = "Super Unleaded - 10% Ethanol", GallonOfGasEquivalent= .9814f, PricePerVolumeUnit= 3.65f, VehicleSpecificDistanceUnitsPerVolumeUnit= 33, KgOfCarbonEmissionsPerVolumeUnit = 8.887 *.90},
                new FuelType{ Name = "E85 - 85% Ethanol", GallonOfGasEquivalent = .7194f, PricePerVolumeUnit= 2.64f, VehicleSpecificDistanceUnitsPerVolumeUnit= 23, KgOfCarbonEmissionsPerVolumeUnit = 8.887 * .15},
                new FuelType{ Name = "Electricity - Coal", GallonOfGasEquivalent = 1/33.4, PricePerVolumeUnit= 3.34, KgOfCarbonEmissionsPerVolumeUnit = .95*33.4, VehicleSpecificDistanceUnitsPerVolumeUnit =98},
                new FuelType{ Name = "Electricity - Natural Gas", GallonOfGasEquivalent = 1/33.4, PricePerVolumeUnit= 3.34, KgOfCarbonEmissionsPerVolumeUnit = .6*33.4, VehicleSpecificDistanceUnitsPerVolumeUnit = 98},
                new FuelType{ Name = "Electricity - Oil", GallonOfGasEquivalent = 1/33.4, PricePerVolumeUnit= 3.34, KgOfCarbonEmissionsPerVolumeUnit = .9*33.4, VehicleSpecificDistanceUnitsPerVolumeUnit=98},
                new FuelType{ Name = "Electricity - Wind,Solar,Etc.", GallonOfGasEquivalent = 1/33.4, PricePerVolumeUnit= 3.34, KgOfCarbonEmissionsPerVolumeUnit = 0, VehicleSpecificDistanceUnitsPerVolumeUnit=98},
            };
        }
        public IList<FuelType> Fuels { get; set; }
    }
}
