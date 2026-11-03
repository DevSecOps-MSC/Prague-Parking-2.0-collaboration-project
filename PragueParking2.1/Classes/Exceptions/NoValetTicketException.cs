namespace PragueParking2.Classes.Exceptions;

public class NoValetTicketException : Exception
{
    public NoValetTicketException()
    {
    }

    public NoValetTicketException(string? message) : base(message)
    {
    }

    public NoValetTicketException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
