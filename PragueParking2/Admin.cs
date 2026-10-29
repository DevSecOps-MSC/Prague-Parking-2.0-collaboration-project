using FileHandlerLibrary;
using PragueParking2.Classes;

namespace PragueParking2;

internal static class Admin
{
    static readonly string defaultConfigJson = """
    {
        "ParkingGarageCapacity": 10,
        "ParkingSpaceCapacity": 4,
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

    internal static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/pp2";
    internal static readonly string baseDirectory = AppContext.BaseDirectory;
    static readonly string configFilename = "config.json";
    static readonly string dataFilename = "data.json";

    internal static bool Setup(FileHandler fileHandler)
    {
        if (fileHandler.DataDirectoryExists())
        {
            // TODO: log!
            Console.WriteLine("Application data directory exists. Continuing to load configuration.");
        }
        else
        {
            // TODO: log!
            Console.WriteLine("It appears that this is the first time this software is running. Attempting to create application data directory.");
            if (fileHandler.CreateDataDirectory(out string message))
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message);
                return false;
            }
        }

        return true;
    }

    internal static Config LoadDefaultConfig()
        => Config.FromJson(defaultConfigJson);


    internal static Config LoadConfig(FileHandler fileHandler)
    {
        try
        {
            return Config.FromFile(fileHandler, configFilename);
        }
        catch (Exception)
        {
            Console.WriteLine("Could not load configuration file. Loading default values.");
            return LoadDefaultConfig();
        }
    }

    internal static ParkingGarage LoadGarage(FileHandler fileHandler, Config config)
    {
        try
        {
            return ParkingGarage.FromFile(fileHandler, dataFilename);
        }
        catch (Exception)
        {
            Console.WriteLine("Could not load garage data file. Building new garage from configuration.");
            return ParkingGarage.FromConfig(config);
        }
    }
}
