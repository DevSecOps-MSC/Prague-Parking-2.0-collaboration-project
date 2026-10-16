using PragueParking2.Classes;
using PragueParking2.Classes.Testing;


// Example of a Simple Test
SimpleTest.AddNewTest(
    "This test is a test!", () =>
    {
        return 2 + 2 == 4; // <-- Any statement that can be evaluated into true or false
    });

SimpleTest.AddNewTest(
    "This test will fail!", () =>
    {
        return true == false;
    });

SimpleTest.AddNewTest(
    "This test will pass!", () =>
    {
        return true == true;
    });

// Actual test that actually tests actual code :)
SimpleTest.AddNewTest(
    "A Parking Space is Empty by default", () =>
    {
        var space = new ParkingSpace();

        return space.IsEmpty == true;
    });


SimpleTest.RunAllTests();

// Display results
foreach (var testResult in SimpleTest.testResults)
{
    if (testResult.Value == true)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(testResult.Key + ": " + testResult.Value);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(testResult.Key + ": " + testResult.Value);
    }

    Console.ResetColor();
}