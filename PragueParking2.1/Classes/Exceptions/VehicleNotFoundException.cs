namespace PragueParking2.Classes.Exceptions;

public class VehicleNotFoundException : Exception
{
    public VehicleNotFoundException()
    {
    }

    public VehicleNotFoundException(string? message) : base(message)
    {
    }

    public VehicleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
