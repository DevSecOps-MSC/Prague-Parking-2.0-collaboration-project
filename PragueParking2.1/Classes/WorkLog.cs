using System.Text.Json;
using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

internal class WorkLog
{
    private List<ILogEntry> _logEntries = [];

    internal int Count => _logEntries.Count;
    internal bool IsEmpty => Count == 0;
    internal List<ILogEntry> AllEntries => _logEntries;
    internal ILogEntry First => _logEntries.First();
    internal ILogEntry Last => _logEntries.Last();
    internal int GetNextId => IsEmpty ? 0 : Count;

    internal int AddEntry(string content)
    {
        int id = GetNextId;
        _logEntries.Add(new WorkLogEntry(id, DateTime.Now, content));
        return id;
    }

    internal int AddEntry(WorkOrder content)
    {
        int id = GetNextId;
        _logEntries.Add(new WorkLogEntry(id, DateTime.Now, content));
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

    internal static WorkLog FromJson(string json)
    {
        var log = new WorkLog();

        var logEntries = JsonSerializer.Deserialize<List<WorkLogEntry>>(json);


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
}
