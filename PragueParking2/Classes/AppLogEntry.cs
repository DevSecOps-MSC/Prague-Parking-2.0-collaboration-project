using Spectre.Console;

namespace PragueParking2.Classes;

public record AppLogEntry(int ID, DateTime CreatedAt, object Content)
{
    public string ToMarkupString()
    {
        if (Content is WorkOrder w)
            return $"[grey]{CreatedAt:yyyy-MM-dd HH:mm}[/] {w.ToMarkupString()}";

        return $"[grey]{CreatedAt:yyyy-MM-dd HH:mm}[/] {Content}";
    }
}