using System.Text.Json;
using System.Text.Json.Serialization;
using FileHandlerLibrary;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingGarage
{
    [JsonInclude]
    public List<ParkingSpace> ParkingSpaces { get; private set; } = [];

    [JsonInclude]
    public int Capacity { get; private set; }

    public ParkingGarage(int capacity, int parkingSpaceCapacity)
    {
        Capacity = capacity;
        InitializeParkingSpaces(parkingSpaceCapacity);
    }

    public ParkingGarage() { }

    internal void InitializeParkingSpaces(int parkingSpaceCapacity)
    {
        ParkingSpaces = [];

        for (int i = 0; i < Capacity; i++)
        {
            ParkingSpaces.Add(new ParkingSpace(i + 1, parkingSpaceCapacity));
        }
    }

    public int FirstEmptySpace()
    {
        foreach (var space in ParkingSpaces)
        {
            if (space.IsEmpty)
            {
                return space.ID;
            }
        }

        return -1;
    }

    internal int FirstAvailableSpace(IVehicle vehicle)
    {
        foreach (var space in ParkingSpaces)
        {
            if (vehicle.Size <= space.AvailableSpace)
            {
                return space.ID;
            }
        }
        return -1;
    }

    public IParkingSpace this[int index]
    {
        get => ParkingSpaces[index - 1];
    }

    public string ToJson()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        return jsonString;
    }

    public static ParkingGarage FromJson(string jsonString)
    {
        ParkingGarage? loadedGarage = JsonSerializer.Deserialize<ParkingGarage>(jsonString);
        if (loadedGarage is null)
        {
            throw new NullReferenceException("Error! Could not parse save file.");
        }
        return loadedGarage;
    }

    public static ParkingGarage FromFile(FileHandler fileHandler, string filename)
        => FromJson(fileHandler.Load(filename));

    public void SaveToFile(FileHandler fileHandler, string filename)
        => fileHandler.Save(filename, ToJson());

    internal static ParkingGarage FromConfig(Config config)
        => new ParkingGarage(config.ParkingGarageCapacity, config.ParkingSpaceCapacity);
}
