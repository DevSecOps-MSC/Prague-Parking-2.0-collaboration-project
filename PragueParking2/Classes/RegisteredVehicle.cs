using PragueParking2.Classes;
using PragueParking2.Interfaces;

namespace PragueParking2.Tests;

public class RegisteredVehicle : IVehicle
{
    public string Registration { get; }

    public RegisteredVehicle(string registration)
    {
        Registration = Validator.SanitizeVehicleRegistration(registration);
    }
}