using System;

namespace PragueParking2.Interfaces;

public interface IParkingSpace
{
    public int Capacity { get; }
    public List<IVehicle> ParkedVehicles { get; }
}

