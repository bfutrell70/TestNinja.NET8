using System.IO;

namespace TestNinja.NET8.Mocking;

public interface IFileReader
{
    string Read(string path);
}

public class FileReader: IFileReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }
}