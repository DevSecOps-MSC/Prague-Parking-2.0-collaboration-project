using FileHandlerLibrary;

namespace FileHandlerLibraryTest;

[TestClass]
public sealed class StorageTest
{
    static string path = "../../../Storage/";

    [TestMethod]
    public void TestReadTextFile()
    {
        var filename = "test.txt";
        var fileHandler = new FileHandler(path + filename);

        string expected = "This is a test!";

        string actual = fileHandler.ReadTextFile();

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestReadTextFileLines()
    {
        var filename = "test2.txt";
        var fileHandler = new FileHandler(path + filename);

        List<string> expectedLines = [
            "This is a test line!",
            "This is the second test line.",
            "And this is the third...",
            ];

        List<string> actualLines = fileHandler.ReadTextFileLines();

        for (int i = 0; i < expectedLines.Count; i++)
        {
            Assert.AreEqual(expectedLines[i], actualLines[i]);
        }
    }
}
