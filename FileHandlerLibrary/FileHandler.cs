using System.Net.Mime;

namespace FileHandlerLibrary;

public class FileHandler
{
    public FileHandler(string filename)
    {
        Filename = filename;
    }

    public string Filename { get; }

    public string ReadTextFile()
    {
        string contents = "";
        
        using var streamReader = new StreamReader(Filename);
        {
            contents = streamReader.ReadToEnd();
        }
        
        return contents;
    }

    public List<string> ReadTextFileLines()
    {
        List<string> contents = [];

        using var streamReader = new StreamReader(Filename);
        {
            var line = streamReader.ReadLine();
            while (line is not null)
            {
                contents.Add(line);
                line = streamReader.ReadLine();
            }
        }

        return contents;
    }
}
