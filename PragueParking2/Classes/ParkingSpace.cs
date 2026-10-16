using System;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingSpace : IParkingSpace
{
    public int ID { get; set; }
    public List<IVehicle> ParkedVehicles { get; set; } = [];

    public bool IsEmpty
    {
        get
        {
            return true;
        }
    }
}
