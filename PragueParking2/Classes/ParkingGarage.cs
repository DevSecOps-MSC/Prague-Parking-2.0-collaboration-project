using System.Text.Json;
using System.Text.Json.Serialization;
using FileHandlerLibrary;
using PragueParking2.Classes.Exceptions;
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

    public int GetIdOfFirstEmptySpace()
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

    internal int GetIdOfFirstAvailableSpace(IVehicle vehicle)
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
        => new(config.ParkingGarageCapacity, config.ParkingSpaceCapacity);

    internal bool FindVehicle(string registration, out int foundSpaceId)
    {
        foreach (var space in ParkingSpaces)
        {
            try
            {
                var vehicle = space.PickVehicle(registration);
                foundSpaceId = space.ID;
                return true;
            }
            catch (VehicleNotFoundException)
            {
                continue;
            }
        }

        foundSpaceId = -1;
        return false;
    }

    internal void Move(IVehicle vehicle, int fromParkingSpaceId, int toParkingSpaceId)
    {
        return;
    }

    internal bool Park(IVehicle vehicle, int spaceId)
    {
        if (FindVehicle(vehicle.Registration, out int foundSpaceId))
            throw new DuplicateVehicleRegistrationException($"A vehicle with this registration is already parked in parking space #{foundSpaceId}!");

        try
        {
            return this[spaceId].Park(vehicle);
        }
        catch (NotEnoughRoomException)
        {
            // TODO: log event
            return false;
        }
        catch (NoValetTicketException)
        {
            // TODO: log event
            return false;
        }

    }

    internal int CountEmptyParkingSpaces()
    {
        return -1;
    }

    internal int CountParkedCars()
    {
        return -1;
    }

    internal int CountParkedMotorcycles()
    {
        return -1;
    }

    internal int CountParkedBicycles()
    {
        return -1;
    }

    internal bool CheckIn(IVehicle vehicle, Dictionary<VehicleType, decimal> rates)
    {
        if (FindVehicle(vehicle.Registration, out int foundSpaceId))
            throw new DuplicateVehicleRegistrationException($"A vehicle with this registration is already parked in parking space #{foundSpaceId}!");

        var assignedSpaceId = GetIdOfFirstAvailableSpace(vehicle);

        if (assignedSpaceId <= 0)
            throw new NotEnoughRoomException("No available space. Vehicle can not be checked in.");

        try
        {
            var ticket = new ValetTicket(vehicle, rates);
            ticket.UpdateParkingSpaceId(assignedSpaceId);
            return true;
        }
        catch (DuplicateValetTicketException e)
        {
            // TODO: log
            Console.WriteLine(e.Message);
        }
        return false;
    }

    internal bool Collect(IVehicle vehicle, int spaceId)
    {
        return this[spaceId].Collect(vehicle);
    }

    internal bool CheckOut(IVehicle vehicle)
    {
        return true;
    }
}
