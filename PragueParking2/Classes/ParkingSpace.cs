using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingSpace : IParkingSpace
{
    public List<IVehicle> ParkedVehicles { get; } = new List<IVehicle>();

    public bool IsEmpty => ParkedVehicles.Count == 0;

    public bool IsNotEmpty => !IsEmpty;

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