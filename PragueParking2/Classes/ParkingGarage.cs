using System;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingGarage
{
    public int Capacity { get; private set; }
    public List<IParkingSpace> ParkingSpaces { get; private set; } = [];
}
