using System;
using FileHandlerLibrary;
using PragueParking2.Interfaces;

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

    internal ParkingGarage ParkingGarage { get => _parkingGarage; private set { _parkingGarage = value; } }


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

    internal void LoadGarageData()
    {
        ParkingGarage = Admin.LoadParkingGarageData(FileHandler, Config, Log);
    }

    internal void AddLogEntry(string logEntry)
    {
        Log.AddEntry(logEntry);
    }

    internal void AddLogEntry(WorkOrder logEntry)
    {
        Log.AddEntry(logEntry);
    }

    internal void AddWorkOrderLogEntry(string verb, IVehicle vehicle, int? parkingSpaceId = null)
    {
        Log.AddEntry(new WorkOrder(verb, vehicle.Type, vehicle.Registration, parkingSpaceId ?? vehicle.Ticket!.ParkingSpaceId));
    }
}
