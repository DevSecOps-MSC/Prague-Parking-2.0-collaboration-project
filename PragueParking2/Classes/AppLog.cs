using System.Text.Json;
using PragueParking2.Interfaces;
using Spectre.Console;

namespace PragueParking2.Classes;

internal class AppLog
{
    private List<AppLogEntry> _logEntries = [];

    internal int Count => _logEntries.Count;
    internal bool IsEmpty => Count == 0;
    internal List<AppLogEntry> AllEntries => _logEntries;
    internal AppLogEntry First => _logEntries.First();
    internal AppLogEntry Last => _logEntries.Last();
    internal int GetNextId => IsEmpty ? 0 : Count;

    internal int AddEntry(string content)
    {
        int id = GetNextId;
        _logEntries.Add(new AppLogEntry(id, DateTime.Now, content));
        return id;
    }

    internal int AddEntry(WorkOrder content)
    {
        int id = GetNextId;
        _logEntries.Add(new AppLogEntry(id, DateTime.Now, content));
        return id;
    }
    
    internal string ToJson()
    {
        List<string> jsonList = [];
        foreach (var entry in AllEntries)
        {
            jsonList.Add($"{JsonSerializer.Serialize(entry)}");
        }
        string json = string.Join(",\n", jsonList);
        return $"[\n{json}\n]";
    }

    internal static AppLog FromJson(string json)
    {
        var log = new AppLog();

        var logEntries = JsonSerializer.Deserialize<List<AppLogEntry>>(json);


        for (int i = 0; i<logEntries!.Count; i++)
        {
            var entry = logEntries[i];
            var content = entry.Content.ToString();
            if (content!.Contains("{\"Verb\""))
            {
                logEntries[i] = entry with { Content = JsonSerializer.Deserialize<WorkOrder>(content!)! };
            }

        }

        log._logEntries.AddRange(logEntries);

        return log;
    }

    internal void DumpToConsole()
    {
        foreach (var entry in AllEntries)
        {
            AnsiConsole.Write(new Markup(entry.ToMarkupString() + "\n"));
        }
    }

    internal string[] TakeLast(int count)
    {
        string[] entries = new string[count];

        foreach (var (entry, i) in _logEntries.TakeLast(count).Select((v, i) => (v, i)))
        {
            entries[i] = entry.ToMarkupString();
        }

        return entries;
    }
}
