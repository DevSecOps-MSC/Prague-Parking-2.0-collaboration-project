using System.Text.Json.Serialization;
using PragueParking2.Classes;
using PragueParking2.Classes.Exceptions;
using PragueParking2.Interfaces;

namespace PragueParking2.Tests;

public class Vehicle(string registration, int size = 1) : IVehicle
{
    [JsonInclude]
    public string Registration { get; } = Validator.SanitizeVehicleRegistration(registration);

    [JsonInclude]
    public int Size { get; } = size;

    [JsonInclude]
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

    public IVehicle SimulateCheckIn(AppConfiguration config)
    {
        var ticket = new ValetTicket(this, config.Rates[Type]);
        return this;
    }

    [JsonIgnore]
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

    internal static IVehicle FromRecord(VehicleRecord record)
    {
        IVehicle vehicle = record.VehicleType switch
        {
            VehicleType.Car => new Car(record.Registration, record.Size),
            VehicleType.Motorcycle => new Motorcycle(record.Registration, record.Size),
            VehicleType.Bus => new Bus(record.Registration, record.Size),
            VehicleType.Bicycle => new Bicycle(record.Registration, record.Size),
            _ => throw new VehicleTypeNotSupportedException(
                "This vehicle type is not supported at the moment")
        };
        Console.WriteLine(record.ValetTicketRecord.TimeOfCheckIn);
        ValetTicket ticket = new(vehicle, record.ValetTicketRecord.Rate, record.ValetTicketRecord.TimeOfCheckIn);
        ticket.UpdateParkingSpaceId(record.ValetTicketRecord.ParkingSpaceId);
        return vehicle;
    }

}