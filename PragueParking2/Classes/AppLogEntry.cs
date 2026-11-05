using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public record AppLogEntry(int ID, DateTime CreatedAt, object Content);