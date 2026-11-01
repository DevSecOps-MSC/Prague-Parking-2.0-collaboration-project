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

    [JsonInclude]
    public int BusCapable { get; private set; }

    public ParkingGarage(int capacity, int parkingSpaceCapacity, int busCapable = 0)
    {
        Capacity = capacity;
        InitializeParkingSpaces(parkingSpaceCapacity);
        BusCapable = busCapable;
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
        var spaceCapacity = ParkingSpaces.First().Capacity;
        if (vehicle.Size > spaceCapacity)
        {
            var firstEmptySpaceId = GetIdOfFirstEmptySpace();
            var neededSpaces = vehicle.Size / spaceCapacity;

            if (firstEmptySpaceId -1 > BusCapable - neededSpaces)
                return -1;
            
            for (int i = firstEmptySpaceId; i < BusCapable; i++)
            {
                for (int j = 1; j < neededSpaces; j++)
                {
                    if (this[i+j].IsNotEmpty)
                        break;
                    
                    if (j == neededSpaces - 1)
                        return i;
                }
            }

            return -1;
        }

        foreach (var space in ParkingSpaces)
        {
            if (space.IsNotEmpty)
            {
                if (space.ParkedVehicles.First().Type != vehicle.Type)
                    continue;
            }

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

    public bool FindVehicle(string registration, out int foundSpaceId)
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
        if (this[toParkingSpaceId].Park(vehicle))
        {
            this[fromParkingSpaceId].Collect(vehicle);
        }
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
        int count = 0;

        foreach (ParkingSpace parkingSpace in ParkingSpaces)
        {
            if (parkingSpace.IsEmpty)
            {
                count = count + 1;
            }
        }
        return count;
    }

    internal int CountParkedCars()
    {
        int count = 0;

        foreach (ParkingSpace parkingSpace in ParkingSpaces)
        {
            parkingSpace.ContainsCars(out int qty);

            count = count + qty;
        }
        return count;
    }

    internal int CountParkedMotorcycles()
    {
        int count = 0;

        foreach (ParkingSpace parkingSpace in ParkingSpaces)
        {
            parkingSpace.ContainsMotorcycles(out int qty);

            count = count + qty;
        }
        return count;
    }

    internal int CountParkedBicycles()
    {
        int count = 0;

        foreach (ParkingSpace parkingSpace in ParkingSpaces)
        {
            parkingSpace.ContainsBicycles(out int qty);

            count = count + qty;
        }

        return count;
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
        if (Collect(vehicle, vehicle.Ticket!.ParkingSpaceId))
        {
            vehicle.Ticket.CheckOut(DateTime.Now);
            return true;
        }

        return false;
    }

    internal string GenerateBicycleRegistration(Random? rng = null)
    {
        rng ??= new Random();
        string registration;

        do
        {
            int id = rng.Next(99999) + 1;
            registration = $"BIKE-{id:00000}";
        } while (FindVehicle(registration, out _) is true);

        return registration;
    }

    internal bool OptimizeParking(VehicleType vehicleType, out List<WorkOrder> workOrders)
    {
        workOrders = [];
        // Scan the parking garage back to front
        for (int originSpaceId = Capacity; originSpaceId >= 1; originSpaceId--)
        {
            var space = this[originSpaceId];

            // Nothing to move
            if (space.IsEmpty || space.IsFull)
                continue;

            // Wrong Vehicle Type
            if (space.ParkedVehicles.Last().Type != vehicleType)
                continue;

            // Pick the Vehicle to move
            var vehicle = space.ParkedVehicles.Last();

            // Get the ID of the first available space to move to
            var destinationSpaceId = GetIdOfFirstAvailableSpace(vehicle);

            // If origin and destination are the same, no further optimization is possible
            if (originSpaceId == destinationSpaceId)
                return true;

            // Move the vehicle
            if (Collect(vehicle, originSpaceId))
            {
                workOrders.Add(new WorkOrder("Collect", vehicleType, vehicle.Registration, originSpaceId));
                if (Park(vehicle, destinationSpaceId))
                {
                    workOrders.Add(new WorkOrder("Park", vehicleType, vehicle.Registration, destinationSpaceId));
                    continue;
                }
                else
                    throw new Exception("Move failed: Could not park vehicle in destination parking space.");
            }
            else
                throw new Exception("Move failed: Could not collect vehicle from origin parking space.");

        }

        return false;
    }
}
