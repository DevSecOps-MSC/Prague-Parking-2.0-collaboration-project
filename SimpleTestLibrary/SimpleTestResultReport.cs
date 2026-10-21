using System;

namespace SimpleTestLibrary;

public static class SimpleTestResultReport
{
    /// <summary>
    /// Formats a List of test results and writes them to the screen
    /// </summary>
    /// <param name="testResults"></param>
    public static void WriteReport(List<TestResult> testResults)
    {
        foreach (var testResult in testResults)
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