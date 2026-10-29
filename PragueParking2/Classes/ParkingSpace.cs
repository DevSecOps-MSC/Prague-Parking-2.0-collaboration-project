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
        
        if (vehicle.Size > AvailableSpace)
        {
            throw new NotEnoughRoomException();
        }

        ParkedVehicles.Add(vehicle);
        return true;
        
    }

    public bool Collect(IVehicle vehicle)
    {
        ParkedVehicles.Remove(vehicle);
        return true;
    }

    public bool Contains(IVehicle vehicle)
    {
        return ParkedVehicles.Contains(vehicle);
    }

    internal bool ContainsCars(out int qty)
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

    internal bool ContainsMotorcycles(out int qty)
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
}