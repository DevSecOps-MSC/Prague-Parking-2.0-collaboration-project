namespace PragueParking2.Classes.Exceptions;

public class DuplicateVehicleRegistrationException : Exception
{
    public DuplicateVehicleRegistrationException()
    {
    }

    public DuplicateVehicleRegistrationException(string? message) : base(message)
    {
    }

    public DuplicateVehicleRegistrationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
