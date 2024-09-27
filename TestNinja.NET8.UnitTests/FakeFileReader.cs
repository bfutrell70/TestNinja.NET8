using TestNinja.NET8.Mocking;

namespace TestNinja.NET8.UnitTests;

public class FakeFileReader: IFileReader
{
    public string Read(string path)
    {
        return "";
    }
}
