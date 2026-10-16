using System;

namespace PragueParking2.Classes.Testing;

public static class SimpleTest
{
    private static Dictionary<string, Func<bool>> tests = [];
    public static Dictionary<string, bool> testResults = [];

/// <summary>
/// Runs once before the first test
/// </summary>
    public static void InitializeTestEnvironment()
    {
        testResults = [];
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
/// Runs once before the first test
/// </summary>
    public static void DestroyTestEnvironment()
    {
        
    }

    private static void Test(string description, Func<bool> test)
    {
        testResults.Add(description, test());
    }

    public static void AddNewTest(string description, Func<bool> test)
    {
        tests.Add(description, test);
    }

    public static void RunAllTests()
    {
        InitializeTestEnvironment();

        foreach (var test in tests)
        {
            SetUp();
            
            Test(test.Key, test.Value);

            TearDown();
        }

        DestroyTestEnvironment();

    }

}
