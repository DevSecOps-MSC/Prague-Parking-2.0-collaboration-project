using System;

namespace PragueParking2.Classes;

internal static class AppConstants
{
    internal const string DataDirectory = "data/";
    internal const string SourceDirectory = "../../../";
    internal const string DataFile = DataDirectory + "data.json";
    internal const string ConfigFile = DataDirectory + "config.json";
    internal const string DefaultConfigJson = """
    {
        "ParkingGarageCapacity": 100,
        "ParkingSpaceCapacity": 4,
        "BusCapableParkingSpaceCapacity": 50,
        "FreeMinutes": 10,
        "VehicleSizes": {
            "Car": 4,
            "Motorcycle": 2
    },
        "Rates": {
            "Car": 20.0,
            "Motorcycle": 10.0
        }
    }
    """;

}
