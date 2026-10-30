using PragueParking2.Interfaces;
using PragueParking2.Tests;

namespace PragueParking2.Classes;

// Note to self: add a ToRecord() or AsRecord() method (or property) for easy logging?

public class ValetTicket
{
    public IVehicle Vehicle { get; }
    public decimal Rate { get; }
    public DateTime TimeOfCheckIn { get; }
    public DateTime? TimeOfCheckOut { get; private set; }
    public int? ParkingSpaceId { get; private set; }

    public ValetTicket(IVehicle vehicle, Dictionary<VehicleType, decimal> rates, DateTime now)
    {
        Vehicle = vehicle;
        Vehicle.AttachTicket(this);
        Rate = rates[vehicle.Type];
        TimeOfCheckIn = now;
    }

    public ValetTicket(IVehicle vehicle, Dictionary<VehicleType, decimal> rates):this(vehicle, rates, DateTime.Now)
    { }

    internal void CheckOut(DateTime now)
    {
        TimeOfCheckOut = now;
    }

    internal void UpdateParkingSpaceId(int newSpaceId)
    {
        ParkingSpaceId = newSpaceId;
    }
}
