using System;

namespace PragueParking2.Interfaces;

public interface IParkingSpace
{
    public List<IVehicle> ParkedVehicles { get; }
}

