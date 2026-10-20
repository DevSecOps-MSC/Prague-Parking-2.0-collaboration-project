using FileHandlerLibrary;
using PragueParking2.Classes;

// If available, load Configuration File into a [Config] object
var configFilePath = "../../../config.json";
var jsonString = FileHandler.Load(configFilePath);
var config = Config.FromJson(jsonString);

System.Console.WriteLine($"Garage Capacity: {config.ParkingGarageCapacity}");
System.Console.WriteLine($"Parking Space Capacity: {config.ParkingSpaceCapacity}");
System.Console.WriteLine($"Free Minutes: {config.FreeMinutes}");
System.Console.WriteLine("Vehicle Size Chart:");
foreach (var vehicleSize in config.VehicleSizes)
{
    System.Console.WriteLine($"- {vehicleSize.Key}: {vehicleSize.Value}");
}
System.Console.WriteLine("Current Rates:");
foreach (var rate in config.Rates)
{
    System.Console.WriteLine($"- {rate.Key}: {rate.Value:F2} / hour");
}