using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingSpace : IParkingSpace
{
    public int ID => throw new NotImplementedException();

    public List<IVehicle> ParkedVehicles { get; } = new List<IVehicle>();

    public bool IsEmpty => ParkedVehicles.Count == 0;

    public bool IsNotEmpty => !IsEmpty;
}