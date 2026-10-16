using System;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class StandardParkingSpace : IParkingSpace
{
    public int ID { get; set; }
    public List<IVehicle> ParkedVehicles { get; set; } = [];
}
