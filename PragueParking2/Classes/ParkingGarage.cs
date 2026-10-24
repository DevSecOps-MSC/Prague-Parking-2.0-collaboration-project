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

    public int FirstEmptySpace()
    {
        return -1;
    }

    internal int FirstAvailableSpace(IVehicle vehicle)
    {
        return -1;
    }

    public IParkingSpace this[int index]
    {
        get => ParkingSpaces[index + 1];
    }
}
