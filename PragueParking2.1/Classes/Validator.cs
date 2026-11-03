using System.Text.RegularExpressions;

namespace PragueParking2.Classes;

public class Validator
{
    public static bool VehicleRegistrationIsValid(string registration)
    {
        if (string.IsNullOrWhiteSpace(registration))
            return false;

        if (registration.Length > 10)
            return false;

        var pattern = @"^(\w| |-)*\w$";

        if (Regex.IsMatch(registration, pattern))
            return true;

        return false;
    }

    public static string SanitizeVehicleRegistration(string input)
    {
        return input.Trim().ToUpper();
    }
}
