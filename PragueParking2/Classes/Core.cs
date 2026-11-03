using System;
using FileHandlerLibrary;

namespace PragueParking2.Classes;

internal class Core(FileHandler fileHandler, AppConfiguration config, ParkingGarage parkingGarage, AppLog log)
{
    private AppConfiguration _config = config;
    private FileHandler _fileHandler = fileHandler;
    private AppLog _log = log;
    private ParkingGarage _parkingGarage = parkingGarage;

    internal AppConfiguration Config { get => _config; }

    internal FileHandler FileHandler { get => _fileHandler; }

    internal AppLog Log { get => _log; }

    internal ParkingGarage ParkingGarage { get => _parkingGarage; }


    internal bool SaveConfiguration()
    {
        try
        {
            FileHandler.Save(AppConstants.ConfigFile, Config.ToJson());
            Log.AddEntry("Configuration file saved!");
            return true;
        }
        catch (Exception)
        {
            Log.AddEntry("Configuration file could not be saved!");
        }

        return false;
    }

    internal bool SaveGarageData()
    {
        try
        {
            FileHandler.Save(AppConstants.DataFile, ParkingGarage.ToJson());
            Log.AddEntry("Parking garage data file saved!");
            return true;
        }
        catch (Exception)
        {
            Log.AddEntry("Parking garage data file could not be saved!");
        }

        return false;
    }

}
