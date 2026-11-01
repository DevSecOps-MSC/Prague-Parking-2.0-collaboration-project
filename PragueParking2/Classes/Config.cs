using System.Text.Json;
using System.Text.Json.Serialization;
using FileHandlerLibrary;

namespace PragueParking2.Classes;

public class Config
{
    [JsonInclude]
    public int ParkingGarageCapacity { get; private set; }

    [JsonInclude]
    public int ParkingSpaceCapacity { get; private set; }

    [JsonInclude]
    public int BusCapableParkingSpaceCapacity { get; private set; }

    [JsonInclude]
    public int FreeMinutes { get; private set; }

    [JsonInclude]
    public Dictionary<VehicleType, int> VehicleSizes { get; private set; } = [];

    [JsonInclude]
    public Dictionary<VehicleType, decimal> Rates { get; private set; } = [];

    public Config(int garageCapacity, int spaceCapacity, int busCapableParkingSpaceCapacity, int freeMinutes)
    {
        ParkingGarageCapacity = garageCapacity;
        ParkingSpaceCapacity = spaceCapacity;
        BusCapableParkingSpaceCapacity = Math.Min(busCapableParkingSpaceCapacity, garageCapacity);
        FreeMinutes = freeMinutes;

    }

    public Config() { }

    public string ToJson()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        return jsonString;
    }

    public static Config FromJson(string jsonString)
        => JsonSerializer.Deserialize<Config>(jsonString)
            ?? throw new Exception();

    public static Config FromFile(FileHandler fileHandler, string filename)
        => FromJson(fileHandler.Load(filename));

    public void SaveToFile(FileHandler fileHandler, string filename)
        => fileHandler.Save(filename, ToJson());
}
