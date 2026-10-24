using System;

namespace PragueParking2.Interfaces;

public interface IParkingSpace
{
    public int ID { get; }
    public int Capacity { get; }
    public List<IVehicle> ParkedVehicles { get; }

    public bool IsEmpty { get; }
    public bool IsNotEmpty { get; }
    public bool IsFull { get; }
    public int AvailableSpace { get; }

    public bool Park(IVehicle vehicle);
    public bool Collect(IVehicle vehicle);
    public bool Contains(IVehicle vehicle);

}

