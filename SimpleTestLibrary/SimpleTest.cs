namespace SimpleTestLibrary;

public static class SimpleTest
{
    private static List<Test> _tests = [];

    public static List<TestResult> TestResults = [];

    /// <summary>
    /// Runs once before the first test
    /// </summary>
    public static void InitializeTestEnvironment()
    {
        TestResults = [];
    }

    /// <summary>
    /// Runs once before every test
    /// </summary>
    public static void SetUp()
    {

    }

    /// <summary>
    /// Runs once after every test
    /// </summary>
    public static void TearDown()
    {

    }

    /// <summary>
    /// Runs once after the last test
    /// </summary>
    public static void DestroyTestEnvironment()
    {

    }

    public static void AddNewTest(Test test)
    {
        _tests.Add(test);
    }

    public static void RunAllTests()
    {
        InitializeTestEnvironment();

        foreach (var test in _tests)
        {
            SetUp();

            bool result = test.Perform();
            TestResults.Add(new TestResult(test.Description, result));

            TearDown();
        }

        DestroyTestEnvironment();

    }
}
