using FileHandlerLibrary;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;

internal class Program
{
    static readonly string defaultConfigJson = "{\"ParkingGarageCapacity\": 5,\"ParkingSpaceCapacity\": 4,\"FreeMinutes\": 10,\"VehicleSizes\": {\"Car\": 4,\"Motorcycle\": 2},\"Rates\": {\"Car\": 20.0,\"Motorcycle\": 10.0}}";
    static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/pp2";
    // static FileHandler fileHandler = new(path);
    // static string configFilename = "config.json";
    // static string garageFilename = "garage.json";
    static Config? config;
    // static ParkingGarage? parkingGarage;


    private static void Main(string[] args)
    {
        // if (Directory.Exists(path))
        // {
        //     Console.WriteLine($"Directory [{path}] already exists. Attempting to load configuration.");
        // }
        // else
        // {
        //     DirectoryInfo di;
        //     try
        //     {
        //         di = Directory.CreateDirectory(path);
        //         Console.WriteLine($"Directory [{path}] was created successfully!");
        //     }
        //     catch (UnauthorizedAccessException e)
        //     {
        //         Console.WriteLine($"It appears the the user does not have the required permission to create directory [{path}]");
        //         Console.WriteLine(e.Message);
        //     }
        // }

        // try
        // {
        //     config = Config.FromFile(fileHandler, configFilename);
        //     Console.WriteLine("Configuration loaded!");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e.Message);
        //     Console.WriteLine("Could not load configuration. Loading default values.");
        //     config = Config.FromJson(defaultConfigJson);
        // }

        // try
        // {
        //     parkingGarage = ParkingGarage.FromFile(fileHandler, garageFilename);
        //     Console.WriteLine("Parking garage loaded!");
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e.Message);
        //     Console.WriteLine("Could not load parking garage data. Building new garage from configuration.");
        //     parkingGarage = ParkingGarage.FromConfig(config);
        // }

        // config.SaveToFile(fileHandler, configFilename);
        // parkingGarage.SaveToFile(fileHandler, garageFilename);

        config = Config.FromJson(defaultConfigJson);

        ValidatorTests.AddTests(config);
        VehicleTests.AddTests(config);
        ValetTicketTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);
        ParkingGarageTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);

    }
}