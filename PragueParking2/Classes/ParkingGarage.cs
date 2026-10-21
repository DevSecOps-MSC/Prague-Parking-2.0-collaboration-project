using System;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingGarage
{
    private int _capacity;

    public ParkingGarage(int capacity)
    {
        _capacity=capacity;
    }

    public int Capacity
    {
        get
        {
            return _capacity;
        }
    }

    // public int Capacity { get; private set; }
    // public List<IParkingSpace> ParkingSpaces { get; private set; } = [];
}
