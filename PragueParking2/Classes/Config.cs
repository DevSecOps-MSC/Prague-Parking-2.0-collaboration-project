using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PragueParking2.Classes;

public class Config
{
    [JsonInclude]
    public int ParkingGarageCapacity { get; private set; }

    [JsonInclude]
    public int ParkingSpaceCapacity { get; private set; }

    [JsonInclude]
    public int FreeMinutes { get; private set; }

    [JsonInclude]
    public Dictionary<VehicleType, int> VehicleSizes { get; private set; } = [];

    [JsonInclude]
    public Dictionary<VehicleType, decimal> Rates { get; private set; } = [];

    public Config(int garageCapacity, int spaceCapacity, int freeMinutes)
    {
        ParkingGarageCapacity = garageCapacity;
        ParkingSpaceCapacity = spaceCapacity;
        FreeMinutes = freeMinutes;

        VehicleSizes.Add(VehicleType.Car, 4);
        VehicleSizes.Add(VehicleType.Motorcycle, 2);

        Rates.Add(VehicleType.Car, 20.0M);
        Rates.Add(VehicleType.Motorcycle, 10.0M);
    }

    public Config() { }

    public string ToJson()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        return jsonString;
    }

    public static Config FromJson(string jsonString)
    {
        Config? loadedConfig = JsonSerializer.Deserialize<Config>(jsonString);
        if (loadedConfig is null)
        {
            throw new NullReferenceException("Error! Could not parse configuration file.");
        }
        return loadedConfig;
    }
}
