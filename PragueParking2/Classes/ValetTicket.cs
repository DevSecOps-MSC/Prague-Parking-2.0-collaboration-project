using PragueParking2.Interfaces;
using PragueParking2.Tests;

namespace PragueParking2.Classes;

// Note to self: add a ToRecord() or AsRecord() method (or property) for easy logging?

public class ValetTicket
{
    internal TimeSpan TotalTimeParked
    {
        get
        {
            if (TimeOfCheckOut is not null)
                return (TimeSpan)(TimeOfCheckOut - TimeOfCheckIn);
            else
                return DateTime.Now - TimeOfCheckIn;
        }
    }

    internal int TotalMinutesParked
        => (int)Math.Ceiling(TotalTimeParked.TotalMinutes);

    internal int TotalHoursParked
        => (int)Math.Ceiling(TotalTimeParked.TotalHours);

    // public IVehicle Vehicle { get; }
    public decimal Rate { get; }
    public DateTime TimeOfCheckIn { get; }
    public DateTime? TimeOfCheckOut { get; private set; }
    public int ParkingSpaceId { get; private set; }

    public ValetTicket(IVehicle vehicle, decimal rate, DateTime timeOfCheckIn)
    {
        // Vehicle = vehicle;
        vehicle.AttachTicket(this);
        Rate = rate;
        TimeOfCheckIn = timeOfCheckIn;
    }

    public ValetTicket(IVehicle vehicle, decimal rate):this(vehicle, rate, DateTime.Now)
    { }

    internal void CheckOut(DateTime now)
    {
        TimeOfCheckOut = now;
    }

    internal void UpdateParkingSpaceId(int newSpaceId)
    {
        ParkingSpaceId = newSpaceId;
    }

    internal string TimeOfCheckInDisplay
        =>  $"{TimeOfCheckIn:yyyy-MM-dd HH:mm}";

    internal string TimeOfCheckOutDisplay
        =>  $"{TimeOfCheckOut:yyyy-MM-dd HH:mm}";
}
