
namespace FileHandlerLibrary;

public class FileHandler(string path)
{
    public string Path { get; } = path;

    public string Load(string filename)
        => File.ReadAllText($"{Path}/{filename}")
            ?? throw new Exception();

    public void Save(string filename, string contents)
        => File.WriteAllText($"{Path}/{filename}", contents);

}
