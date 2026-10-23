using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class ParkingGarage
{
    public List<IParkingSpace> ParkingSpaces { get; private set; } = [];

    public int Capacity { get; private set; }

    public ParkingGarage(int capacity, int parkingSpaceCapacity)
    {
        Capacity = capacity;
        InitializeParkingSpaces(parkingSpaceCapacity);
    }

    internal void InitializeParkingSpaces(int parkingSpaceCapacity)
    {
        ParkingSpaces = [];

        for (int i = 0; i < Capacity; i++)
        {
            ParkingSpaces.Add(new ParkingSpace(parkingSpaceCapacity));
        }
    }
}
