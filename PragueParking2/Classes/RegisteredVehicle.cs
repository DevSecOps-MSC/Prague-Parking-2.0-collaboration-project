using PragueParking2.Classes;
using PragueParking2.Interfaces;

namespace PragueParking2.Tests;

public class RegisteredVehicle : IVehicle
{
    public string Registration { get; }

    public int Size { get; }

    public RegisteredVehicle(string registration, int size = 0)
    {
        Registration = Validator.SanitizeVehicleRegistration(registration);
    }
}