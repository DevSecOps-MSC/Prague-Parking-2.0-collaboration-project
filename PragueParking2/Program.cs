using FileHandlerLibrary;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;

internal class Program
{
    static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/pp2";
    static readonly string defaultConfigJson = "{\"ParkingGarageCapacity\": 5,\"ParkingSpaceCapacity\": 4,\"FreeMinutes\": 10,\"VehicleSizes\": {\"Car\": 4,\"Motorcycle\": 2},\"Rates\": {\"Car\": 20.0,\"Motorcycle\": 10.0}}";
    static Config config = Config.FromJson(defaultConfigJson);
    static ParkingGarage parkingGarage = ParkingGarage.FromConfiguration(config);

    private static void Main(string[] args)
    {
        if (Directory.Exists(path))
        {
            Console.WriteLine($"Directory [{path}] already exists. Attempting to load configuration.");
        }
        else
        {
            DirectoryInfo di;

            try
            {
                di = Directory.CreateDirectory(path);
                Console.WriteLine($"Directory [{path}] was created successfully!");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"It appears the the user does not have the required permission to create directory [{path}]");
                Console.WriteLine(e.Message);
            }
        }

        var configFilePath = path + "/config.json";
        string configJson;
        try
        {
            configJson = FileHandler.Load(configFilePath);
            Console.WriteLine("Configuration file loaded!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Configuration file not found, or it could not be parsed properly. Loading default values.");
            configJson = "{\"ParkingGarageCapacity\": 100,\"ParkingSpaceCapacity\": 4,\"FreeMinutes\": 10,\"VehicleSizes\": {\"Car\": 4,\"Motorcycle\": 2},\"Rates\": {\"Car\": 20.0,\"Motorcycle\": 10.0}}";
        }

        try
        {
            config = Config.FromJson(configJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Warning: Problem parsing configuration file!");
        }

        if (File.Exists(configFilePath) is not true)
        {

            try
            {
                FileHandler.Save(configFilePath, config.ToJson());
                Console.WriteLine($"New configuration file saved at: {configFilePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Warning: Problem saving configuration file!");
            }
        }

        var garageFilePath = path + "/garage.json";
        string garageJson = "";
        try
        {
            garageJson = FileHandler.Load(garageFilePath);
            Console.WriteLine("Garage file loaded!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Garage file not found.");
        }

        try
        {
            parkingGarage = ParkingGarage.FromJson(garageJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Warning: Problem parsing garage file!");
        }



        ValidatorTests.AddTests(config);
        RegisteredVehicleTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);
        ParkingGarageTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);

    }
}