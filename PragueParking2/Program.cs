using FileHandlerLibrary;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        // If available, load Configuration File into a [Config] object
        var configFilePath = "../../../config.json";
        string jsonString;
        if (File.Exists(configFilePath))
        {
            jsonString = FileHandler.Load(configFilePath);
        }
        else
        {
            try
            {
                jsonString = FileHandler.Load("config.json");            
            }
            catch (Exception)
            {
                Console.WriteLine("Configuration file not found. Loading default values.");
                jsonString = "{\"ParkingGarageCapacity\": 100,\"ParkingSpaceCapacity\": 4,\"FreeMinutes\": 10,\"VehicleSizes\": {\"Car\": 4,\"Motorcycle\": 2},\"Rates\": {\"Car\": 20.0,\"Motorcycle\": 10.0}}";
            }
        }
        var config = Config.FromJson(jsonString);

// System.Console.WriteLine($"Garage Capacity: {config.ParkingGarageCapacity}");
// System.Console.WriteLine($"Parking Space Capacity: {config.ParkingSpaceCapacity}");
// System.Console.WriteLine($"Free Minutes: {config.FreeMinutes}");
// System.Console.WriteLine("Vehicle Size Chart:");
// foreach (var vehicleSize in config.VehicleSizes)
// {
//     System.Console.WriteLine($"- {vehicleSize.Key}: {vehicleSize.Value}");
// }
// System.Console.WriteLine("Current Rates:");
// foreach (var rate in config.Rates)
// {
//     System.Console.WriteLine($"- {rate.Key}: {rate.Value:F2} / hour");
// }


        ValidatorTests.AddTests(config);
        RegisteredVehicleTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);
    }
}