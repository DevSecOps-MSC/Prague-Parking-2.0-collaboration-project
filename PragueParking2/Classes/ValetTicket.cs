using PragueParking2.Tests;

namespace PragueParking2.Classes;

// Note to self: add a ToRecord() or AsRecord() method (or property) for easy logging?

public class ValetTicket
{
    public Vehicle Vehicle { get; }
    public decimal Rate { get; }
    public DateTime TimeOfCheckIn { get; }
    public DateTime? TimeOfCheckOut { get; private set; }

    public ValetTicket(Vehicle vehicle, Dictionary<VehicleType, decimal> rates, DateTime now)
    {
        Vehicle = vehicle;
        Vehicle.Ticket = this;
        Rate = rates[vehicle.Type];
        TimeOfCheckIn = now;
    }

    public ValetTicket(Vehicle vehicle, Dictionary<VehicleType, decimal> rates):this(vehicle, rates, DateTime.Now)
    { }

    internal void CheckOut(DateTime now)
    {
        TimeOfCheckOut = now;
    }
}
