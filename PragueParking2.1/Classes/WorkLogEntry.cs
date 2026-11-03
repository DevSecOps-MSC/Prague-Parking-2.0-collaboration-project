using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public record WorkLogEntry(int ID, DateTime CreatedAt, object Content) : ILogEntry;