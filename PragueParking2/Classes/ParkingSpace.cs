using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingSpace : IParkingSpace
{

    public ParkingSpace(int capacity)
    {
        Capacity = capacity;
    }

    public List<IVehicle> ParkedVehicles { get; } = new List<IVehicle>();
    public int Capacity { get; }

    public bool IsEmpty => ParkedVehicles.Count == 0;

    public bool IsNotEmpty => !IsEmpty;

    public int AvailableSpace
    {
        get
        {
            return Capacity;
        }
    }
    
    public bool IsFull => false;

    public bool Park(IVehicle vehicle)
    {
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
}