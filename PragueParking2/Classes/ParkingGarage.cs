using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using FileHandlerLibrary;
using PragueParking2.Classes.Exceptions;
using PragueParking2.Interfaces;
using PragueParking2.Tests;


namespace PragueParking2.Classes;

public class ParkingGarage
{
    [JsonInclude]
    public List<ParkingSpace> ParkingSpaces { get; private set; } = [];

    [JsonInclude]
    public int Capacity { get; private set; }

    [JsonInclude]
    public int SpaceCapacity { get; private set; }

    [JsonInclude]
    public int BusCapable { get; private set; }

    public ParkingGarage(int capacity, int spaceCapacity, int busCapable = 0)
    {
        Capacity = capacity;
        SpaceCapacity = spaceCapacity;
        InitializeParkingSpaces(spaceCapacity);
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
                return space.Id;
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

            if (firstEmptySpaceId - 1 > BusCapable - neededSpaces)
                return -1;

            for (int i = firstEmptySpaceId; i < BusCapable; i++)
            {
                for (int j = 1; j < neededSpaces; j++)
                {
                    if (this[i + j].IsNotEmpty)
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
                return space.Id;
            }
        }
        return -1;
    }

    [JsonIgnore]
    public IParkingSpace this[int index]
    {
        get => ParkingSpaces[index - 1];
    }

    public static ParkingGarage FromFile(FileHandler fileHandler, string filename)
        => FromJson(fileHandler.Load(filename));

    public void SaveToFile(FileHandler fileHandler, string filename)
        => fileHandler.Save(filename, ToJson());

    internal static ParkingGarage FromConfig(AppConfiguration config)
        => new(config.ParkingGarageCapacity, config.ParkingSpaceCapacity, config.BusCapableParkingSpaceCapacity);

    public bool FindVehicle(string registration, out int foundSpaceId)
    {
        foreach (var space in ParkingSpaces)
        {
            try
            {
                var vehicle = space.PickVehicle(registration);
                foundSpaceId = space.Id;
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
        if (vehicle.Type == VehicleType.Bus)
            Collect((Bus)vehicle, fromParkingSpaceId);
        else
            Collect(vehicle, fromParkingSpaceId);

        if (vehicle.Type == VehicleType.Bus)
        {
            vehicle.Ticket!.UpdateParkingSpaceId(toParkingSpaceId);
            Park((Bus)vehicle);
        }
        else
            Park(vehicle, toParkingSpaceId);
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

    internal bool Park(Bus bus)
    {
        var spaceCapacity = ParkingSpaces.First().Capacity;
        if (bus.Size > spaceCapacity)
        {
            var firstSpaceId = bus.Ticket!.ParkingSpaceId;
            var neededSpaces = bus.Size / spaceCapacity;
            this[firstSpaceId].ParkedVehicles.Add(bus);

            for (int i = 1; i < neededSpaces; i++)
            {
                var busFragment = new Bus(bus.Registration, bus.Size / neededSpaces);
                var ticket = bus.Ticket;
                busFragment.AttachTicket(ticket);
                this[firstSpaceId + i].ParkBus(busFragment);
            }
            return true;
        }
        return false;
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

    internal bool CheckIn(IVehicle vehicle, decimal rate)
    {
        if (FindVehicle(vehicle.Registration, out int foundSpaceId))
            throw new DuplicateVehicleRegistrationException($"A vehicle with this registration is already parked in parking space #{foundSpaceId}!");

        var assignedSpaceId = GetIdOfFirstAvailableSpace(vehicle);

        if (assignedSpaceId <= 0)
            throw new NotEnoughRoomException("No available space. Vehicle can not be checked in.");

        try
        {
            var ticket = new ValetTicket(vehicle, rate);
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

    internal bool Collect(Bus bus, int spaceId)
    {
        var spaceCapacity = ParkingSpaces.First().Capacity;
        for (int i = 1; i < bus.Size / spaceCapacity; i++)
        {
            this[spaceId + i].ParkedVehicles.Clear();
        }
        return this[spaceId].Collect(bus);
    }

    internal bool CheckOut(IVehicle vehicle)
    {
        if (vehicle.Type == VehicleType.Bus)
        {
            if (Collect((Bus)vehicle, vehicle.Ticket!.ParkingSpaceId))
            {
                vehicle.Ticket.CheckOut(DateTime.Now);
                return true;
            }
        }

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

    public string ToJson()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        List<ParkingSpaceRecord> parkingSpaceRecords = [];
        foreach (var space in ParkingSpaces)
        {
            List<VehicleRecord> parkedVehicleRecords = [];
            foreach (var vehicle in space.ParkedVehicles)
            {
                var ticket = vehicle.Ticket!;
                ValetTicketRecord valetTicketRecord = new(
                    vehicle.Registration, ticket.ParkingSpaceId, ticket.Rate, ticket.TimeOfCheckIn
                );

                parkedVehicleRecords.Add(
                    new VehicleRecord(vehicle.Type, vehicle.Registration, vehicle.Size, valetTicketRecord)
                );
            }
            parkingSpaceRecords.Add(
                            new ParkingSpaceRecord(space.Id, space.Capacity, parkedVehicleRecords)
                        );
        }
        var garageRecord = new ParkingGarageRecord(Capacity, SpaceCapacity, BusCapable, parkingSpaceRecords);
        string jsonString = JsonSerializer.Serialize(garageRecord, options);
        return jsonString;
    }

    public static ParkingGarage FromJson(string jsonString)
    {
        ParkingGarageRecord? loadedGarage = JsonSerializer.Deserialize<ParkingGarageRecord>(jsonString);
        if (loadedGarage is null)
        {
            throw new NullReferenceException("Error! Could not parse save file.");
        }

        ParkingGarage garage = new(loadedGarage.Capacity, loadedGarage.ParkingSpaceCapacity, loadedGarage.BusCapable);
        garage.ParkingSpaces = [];
        foreach (var space in loadedGarage.ParkingSpaceRecords)
        {
            garage.ParkingSpaces.Add(
                new ParkingSpace(space.Id, space.Capacity)
            );

            foreach (var vehicleRecord in space.ParkedVehicles)
            {
                IVehicle vehicle = Vehicle.FromRecord(vehicleRecord);
                garage[space.Id].ParkedVehicles.Add(vehicle);
            }
        }


        return garage;
    }

    internal decimal GetTotalToPay(ValetTicket ticket)
        => (decimal)Math.Ceiling(ticket.TotalTimeParked.TotalHours) * ticket.Rate;
}
