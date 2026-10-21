using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingSpace : IParkingSpace
{
    public int ID => throw new NotImplementedException();

    public List<IVehicle> ParkedVehicles {get; } = [];

    public bool IsEmpty
    {
        get
        {
            return  true;
        }
    }
}
