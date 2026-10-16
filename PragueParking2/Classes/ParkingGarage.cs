using System;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingGarage
{
    public int Capacity { get; set; } = 100;
    public List<IParkingSpace> ParkingSpaces { get; set; } = [];
}
