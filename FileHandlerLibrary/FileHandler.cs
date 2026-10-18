
namespace FileHandlerLibrary;

public class FileHandler
{
    public static string Load(string path)
    {
        return File.ReadAllText(path);
    }

    public static string[] LoadLines(string path)
    {
        return File.ReadAllLines(path);
    }

    public static void Save(string path, string contents)
    {
        File.WriteAllText(path, contents);
    }

    public static void SaveLines(string path, string[] lines)
    {
        File.WriteAllLines(path, lines);
    }
}
