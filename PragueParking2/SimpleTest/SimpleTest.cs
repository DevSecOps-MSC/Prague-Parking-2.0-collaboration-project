namespace PragueParking2.SimpleTest;

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

    public static void WriteReport()
    {
        foreach (var testResult in TestResults)
        {
            if (testResult.IsPositive)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(testResult.Description + " → " + testResult.Result);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(testResult.Description + " → " + testResult.Result);
            }

        }
            Console.ResetColor();
    }
}
