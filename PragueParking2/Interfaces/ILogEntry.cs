namespace PragueParking2.Interfaces;

public interface ILogEntry
{
    public int ID { get; }
    public DateTime CreatedAt { get; }
    public object Content { get; }
}
