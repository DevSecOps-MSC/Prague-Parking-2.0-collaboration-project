using System.Text.Json.Serialization;
using PragueParking2.Classes.Exceptions;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingSpace : IParkingSpace
{

    public ParkingSpace(int capacity)
    {
        Capacity = capacity;
    }

    public ParkingSpace() { }
    public ParkingSpace(int id, int capacity) : this(capacity)
    {
        ID = id;
    }

    [JsonInclude]
    public int ID { get; }

    [JsonInclude]
    public List<IVehicle> ParkedVehicles { get; } = new List<IVehicle>();
    [JsonInclude]
    public int Capacity { get; }

    [JsonIgnore]
    public bool IsEmpty => ParkedVehicles.Count == 0;

    [JsonIgnore]
    public bool IsNotEmpty => !IsEmpty;

    [JsonIgnore]
    public int AvailableSpace
    {
        get
        {
            int occupiedSpace = 0;

            foreach (IVehicle vehicle in ParkedVehicles)
            {
                occupiedSpace = occupiedSpace + vehicle.Size;
            }

            return Capacity - occupiedSpace;
        }
    }

    [JsonIgnore]
    public bool IsFull => AvailableSpace == 0;

    public bool Park(IVehicle vehicle)
    {
        if (vehicle.Ticket is null)
            throw new NoValetTicketException("A vehicle must be checked in and given a valet ticket before it can be parked.");

        if (vehicle.Size > AvailableSpace)
            throw new NotEnoughRoomException();

        ParkedVehicles.Add(vehicle);
        vehicle.Ticket.UpdateParkingSpaceId(ID);
        return true;
    }

    public bool Collect(IVehicle vehicle)
    {
        return ParkedVehicles.Remove(vehicle);
    }

    public bool Contains(IVehicle vehicle)
    {
        return ParkedVehicles.Contains(vehicle);
    }

    public bool ContainsCars(out int qty)
    {
        qty = 0;

        foreach (IVehicle vehicle in ParkedVehicles)
        {

            if (vehicle is Car)

            {

                qty = qty + 1;

            }
        }

        return qty > 0;
    }

    public bool ContainsMotorcycles(out int qty)
    {
        qty = 0;

        foreach (IVehicle vehicle in ParkedVehicles)

        {

            if (vehicle is Motorcycle)
            {

                qty = qty + 1;

            }

        }

        return qty > 0;
    }

    public bool ContainsBicycles(out int qty)
    {
        qty = 0;

        foreach (IVehicle vehicle in ParkedVehicles)
        {
            if (vehicle is Bicycle)
            {
                qty = qty + 1;
            }
        }

        return qty > 0;
    }

    public bool ContainsBus()
    {
        return ParkedVehicles.First().Type == VehicleType.Bus;
    }

    public IVehicle PickVehicle(string registration)
    {
        registration = Validator.SanitizeVehicleRegistration(registration);
        foreach (var vehicle in ParkedVehicles)
        {
            if (vehicle.Registration.Equals(registration))
                return vehicle;
        }

        throw new VehicleNotFoundException("Vehicle not found! Double check vehicle registration.");
    }

    public void ParkBus(Bus busFragment)
    {
        ParkedVehicles.Add(busFragment);
    }
}