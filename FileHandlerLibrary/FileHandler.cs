
namespace FileHandlerLibrary;

public class FileHandler(string path)
{
    public string Path { get; } = path;

    public string Load(string filename)
        => File.ReadAllText($"{Path}{filename}");

    public void Save(string filename, string contents)
    {
        try
        {
        File.WriteAllText($"{Path}{filename}", contents);
            
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public string[] LoadLines(string filename)
        => File.ReadAllLines($"{Path}{filename}");

    public void SaveLines(string filename, List<string> contents)
        => File.WriteAllLines($"{Path}{filename}", contents);

    public bool DirectoryExists(string directoryName)
        => Directory.Exists(Path + directoryName);

    public bool FileExists(string fileName)
        => File.Exists(Path + fileName);

    public bool CreateDirectory(string directoryName, out string message)
    {
        string directory = $"{Path}{directoryName}";
        DirectoryInfo di;
        try
        {
            di = Directory.CreateDirectory(directory);
            message = $"Directory [{directory}] was created successfully!";
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            message = $"It appears the user does not have the required permission to create directory [{directory}].";
            return false;
        }
    }

    public bool DeleteDirectory(string directoryName, out string message)
    {
        string directory = $"{Path}{directoryName}";
        try
        {
            Directory.Delete(directory, true);
            message = $"Directory [{directory}] was deleted successfully!";
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            message = $"It appears the user does not have the required permission to delete directory [{directory}].";
            return false;
        }
    }
}
