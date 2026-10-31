
namespace FileHandlerLibrary;

public class FileHandler(string path)
{
    public string Path { get; } = path;

    public bool CreateDataDirectory(out string message)
    {
        DirectoryInfo di;
        try
        {
            di = Directory.CreateDirectory(Path);
            message = $"Directory [{Path}] was created successfully!";
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            message = $"It appears the the user does not have the required permission to create directory [{Path}]";
            return false;
        }
    }

    public string Load(string filename)
        => File.ReadAllText($"{Path}/{filename}");

    public void Save(string filename, string contents)
        => File.WriteAllText($"{Path}/{filename}", contents);

    public string[] LoadLines(string filename)
        => File.ReadAllLines($"{Path}/{filename}");

    public void SaveLines(string filename, List<string> contents)
        => File.WriteAllLines($"{Path}/{filename}", contents);

    public bool DataDirectoryExists()
        => Directory.Exists(Path);
}
