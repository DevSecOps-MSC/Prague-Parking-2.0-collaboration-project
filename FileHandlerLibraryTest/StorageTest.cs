using FileHandlerLibrary;

namespace FileHandlerLibraryTest;

[TestClass]
public sealed class StorageTest
{
    static readonly string path = "../../../Storage/";
    static readonly string testFile = path + "test.txt";
    static readonly string testFile2 = path + "test2.txt";
    static readonly string testFile3 = path + "test3.txt";
    static readonly string testFile4 = path + "test4.txt";


    static readonly string testString = "This is a test!";
    static readonly string[] testLines = [
        "This is a test line!",
        "This is the second test line.",
        "And this is the third...",
        ];

    [ClassInitialize]
    public static void InitializeStorageTest(TestContext testContext)
    {
        File.WriteAllText(testFile, testString);
        File.WriteAllLines(testFile2, testLines);
        if (File.Exists(testFile3))
            File.Delete(testFile3);
        if (File.Exists(testFile4))
            File.Delete(testFile4);
    }

    [TestMethod]
    public void TestLoad()
    {
        string expected = testString;

        string actual = FileHandler.Load(testFile);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestLoadLines()
    {
        string[] expected = testLines;

        string[] actual = FileHandler.LoadLines(testFile2);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], actual[i]);
        }
    }

    [TestMethod]
    public void TestSave()
    {
        string expected = testString;

        FileHandler.Save(testFile3, expected);

        Assert.IsTrue(File.Exists(testFile3));

        string actual = File.ReadAllText(testFile3);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestSaveLines()
    {
        string[] expected = testLines;

        FileHandler.SaveLines(testFile4, expected);

        Assert.IsTrue(File.Exists(testFile4));

        string[] actual = FileHandler.LoadLines(testFile4);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], actual[i]);
        }
    }

}
