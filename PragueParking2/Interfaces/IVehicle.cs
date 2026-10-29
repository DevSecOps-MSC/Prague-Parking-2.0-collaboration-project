using System;
using PragueParking2.Classes;

namespace PragueParking2.Interfaces;

public interface IVehicle
{
    public string Registration { get; }
    public int Size { get; }
    public ValetTicket? Ticket { get; }
    public VehicleType Type { get; }
    public void AttachTicket(ValetTicket ticket);
}

