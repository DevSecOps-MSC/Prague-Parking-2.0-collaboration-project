namespace PragueParking2.Classes.Exceptions;

public class DuplicateValetTicketException : Exception
{
    public DuplicateValetTicketException()
    {
    }

    public DuplicateValetTicketException(string? message) : base(message)
    {
    }

    public DuplicateValetTicketException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
