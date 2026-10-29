using PragueParking2.Classes;
using PragueParking2.Classes.Exceptions;
using PragueParking2.Interfaces;

namespace PragueParking2.Tests;

public class Vehicle(string registration, int size = 1) : IVehicle
{
    public string Registration { get; } = Validator.SanitizeVehicleRegistration(registration);

    public int Size { get; } = size;
    public ValetTicket? Ticket { get; internal set; } = null;

    public void AttachTicket(ValetTicket ticket)
    {
        if (Ticket is null)
            Ticket = ticket;
        else
        {
            throw new DuplicateValetTicketException("A vehicle can only be given one valet ticket!");
        }
    }

    public IVehicle SimulateCheckIn(Config config)
    {
        var ticket = new ValetTicket(this, config.Rates);
        return this;
    }

    public VehicleType Type
        => this switch
        {
            Car => VehicleType.Car,
            Motorcycle => VehicleType.Motorcycle,
            Bicycle => VehicleType.Bicycle,
            Bus => VehicleType.Bus,
            _ => throw new VehicleTypeNotSupportedException(
                "This vehicle type is not supported at the moment")
        };

}