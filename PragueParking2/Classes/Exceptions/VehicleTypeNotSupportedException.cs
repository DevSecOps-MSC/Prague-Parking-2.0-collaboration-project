namespace PragueParking2.Classes.Exceptions;

public class VehicleTypeNotSupportedException : Exception
{
    public VehicleTypeNotSupportedException()
    {
    }

    public VehicleTypeNotSupportedException(string? message) : base(message)
    {
    }

    public VehicleTypeNotSupportedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
