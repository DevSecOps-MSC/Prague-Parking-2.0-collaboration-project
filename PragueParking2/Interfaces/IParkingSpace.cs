using System;

namespace PragueParking2.Interfaces;

public interface IParkingSpace
{
    public int ID { get; }
    public List<IVehicle> ParkedVehicles { get; }
}

