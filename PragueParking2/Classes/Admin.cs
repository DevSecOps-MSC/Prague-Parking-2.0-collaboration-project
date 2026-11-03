using FileHandlerLibrary;

namespace PragueParking2.Classes;

internal static class Admin
{
    internal static string path ="";
    internal static readonly string BaseDirectory = AppContext.BaseDirectory;

    internal static Core SetupApp()
    {
        FileHandler fileHandler = new(BaseDirectory);
        AppLog log = new();
        log.AddEntry("Setting up application.");

        if (SetupDataDirectory(fileHandler, log) is false)
        {
            log.AddEntry("Something went horribly wrong and it appears impossible to set up the software. Exiting.");
            log.DumpToConsole();
            Environment.Exit(666);
        }

        AppConfiguration config = LoadConfiguration(fileHandler, log);

        ParkingGarage garage = LoadParkingGarageData(fileHandler, config, log);

        return new(fileHandler, config, garage, log);
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
            log.AddEntry("Configuration file found. Proceeding with attempt to load it.");
            try
            {
                var config = AppConfiguration.FromFile(fileHandler, AppConstants.ConfigFile);
                log.AddEntry("Configuration file loaded successfully!");
                return config;
            }
            catch (Exception )
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
            log.AddEntry("Parking garage data file found. Proceeding with attempt to load it.");
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




