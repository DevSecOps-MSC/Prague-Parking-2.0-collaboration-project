using PragueParking2.Classes;
using PragueParking2.Interfaces;

namespace PragueParking2.Tests;

public class Vehicle : IVehicle
{
    public string Registration { get; }

    public int Size { get; }

    public Vehicle(string registration, int size = 0)
    {
        Registration = Validator.SanitizeVehicleRegistration(registration);
        Size = size;
    }
}