namespace PragueParking2.Classes.Exceptions;

public class NotEnoughRoomException : Exception
{
    public NotEnoughRoomException()
    {
    }

    public NotEnoughRoomException(string? message) : base(message)
    {
    }

    public NotEnoughRoomException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

}
