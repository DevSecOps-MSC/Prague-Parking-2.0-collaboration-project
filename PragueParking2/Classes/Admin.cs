using FileHandlerLibrary;
using PragueParking2.Tests;
using SimpleTestLibrary;

namespace PragueParking2.Classes;

internal static class Admin
{
    internal static string path = "";
    internal static readonly string BaseDirectory = AppContext.BaseDirectory;

    internal static Core SetupApp(string[] args)
    {
        FileHandler fileHandler = new(BaseDirectory);
        AppLog log = new();

        if (args.Contains("--fresh"))
            ClearAppData(fileHandler, log);


        log.AddEntry("Setting up application.");

        if (SetupDataDirectory(fileHandler, log) is false)
        {
            log.AddEntry("Something went horribly wrong and it appears impossible to set up the software. Exiting.");
            log.DumpToConsole();
            Environment.Exit(666);
        }

        AppConfiguration config = LoadConfiguration(fileHandler, log);

        if (args.Contains("--run-tests"))
            RunTests(fileHandler, config, log);

        ParkingGarage garage = LoadParkingGarageData(fileHandler, config, log);

        return new(fileHandler, config, garage, log);
    }

    private static void RunTests(FileHandler fileHandler, AppConfiguration config, AppLog log)
    {
        ValidatorTests.AddTests(config);
        VehicleTests.AddTests(config);
        ValetTicketTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);
        ParkingGarageTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);

        Environment.Exit(0);
    }

    private static void ClearAppData(FileHandler fileHandler, AppLog log)
    {
        if (fileHandler.DirectoryExists(AppConstants.DataDirectory))
        {
            log.AddEntry("Deleting data directory!");
            bool result = fileHandler.DeleteDirectory(AppConstants.DataDirectory, out string message);
            log.AddEntry(message);
        }
    }

    internal static bool SetupDataDirectory(FileHandler fileHandler, AppLog log)
    {
        if (fileHandler.DirectoryExists(AppConstants.DataDirectory))
        {
            log.AddEntry("Data directory exists.");
            return true;
        }
        else
        {
            log.AddEntry("Data directory not found. Proceeding with attempt to create it.");
            bool result = fileHandler.CreateDirectory(AppConstants.DataDirectory, out string message);
            log.AddEntry(message);
            return result;
        }
    }

    internal static AppConfiguration LoadDefaultConfig()
        => AppConfiguration.FromJson(AppConstants.DefaultConfigJson);


    internal static AppConfiguration LoadConfiguration(FileHandler fileHandler, AppLog log)
    {
        if (fileHandler.FileExists(AppConstants.ConfigFile))
        {
            try
            {
                var config = AppConfiguration.FromFile(fileHandler, AppConstants.ConfigFile);
                log.AddEntry("Configuration file loaded successfully!");
                return config;
            }
            catch (Exception)
            {
                log.AddEntry("Could not load configuration file. Loading default values.");
            }
        }
        else
        {
            log.AddEntry("Configuration file not found. Loading default values.");
        }

        return LoadDefaultConfig();
    }

    internal static ParkingGarage LoadParkingGarageData(FileHandler fileHandler, AppConfiguration config, AppLog log)
    {
        if (fileHandler.FileExists(AppConstants.DataFile))
        {
            try
            {
                var garage = ParkingGarage.FromFile(fileHandler, AppConstants.DataFile);
                log.AddEntry("Parking garage data file loaded successfully!");
                return garage;
            }
            catch (Exception)
            {
                log.AddEntry("Could not load parking garage data file. Building new garage from configuration.");
            }
        }
        else
        {
            log.AddEntry("Parking garage data file not found. Building new garage from configuration.");
        }

        return ParkingGarage.FromConfig(config);
    }
}




