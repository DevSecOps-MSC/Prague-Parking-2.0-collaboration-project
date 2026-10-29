using FileHandlerLibrary;
using PragueParking2;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;

internal class Program
{
    static readonly FileHandler fileHandler = new(Admin.path);
    static Config? config;
    static ParkingGarage? parkingGarage;

    private static void Main(string[] args)
    {
        if (Admin.Setup(fileHandler) is false)
        {
            Console.WriteLine("Something horrible went wrong and it appears impossible to set up the software. Exiting.");
            Environment.Exit(666);
        }
        config = Admin.LoadConfig(fileHandler);
        parkingGarage = Admin.LoadGarage(fileHandler,config);


        // config.SaveToFile(fileHandler, configFilename);
        // parkingGarage.SaveToFile(fileHandler, garageFilename);

 
        ValidatorTests.AddTests(config);
        VehicleTests.AddTests(config);
        ValetTicketTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);
        ParkingGarageTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);

    }
}