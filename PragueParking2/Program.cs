using PragueParking2.Classes;
using PragueParking2.Classes.Testing;
using PragueParking2.Interfaces;


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

// Actual tests that actually tests actual code :)
SimpleTest.AddNewTest(
    "A Parking Space is Empty by default", () =>
    {
        var parkingSpace = new ParkingSpace();

        return parkingSpace.IsEmpty == true;
    });

SimpleTest.AddNewTest(
    "A Parking Garage is created with a Capacity", () =>
    {
        var expected = 10;
        var garage = new ParkingGarage(expected);

        return garage.Capacity == expected;
    });

// SimpleTest.AddNewTest(
//     "A Parking Garage has a List of Parking Spaces", () =>
//     {
//         var garage = new ParkingGarage(10);

//         return garage.ParkingSpaces.First() is IParkingSpace;
//     });

// SimpleTest.AddNewTest(
//     "A Parking Garage has a number of Parking Spaces corresponding to its Capacity", () =>
//     {
//         var expected = 10;
//         var garage = new ParkingGarage(expected);

//         return garage.ParkingSpaces.Count == expected;
//     });

// SimpleTest.AddNewTest(
//     "A Parking Garage can be Initialized with Empty Parking Spaces", () =>
//     {
//         var garage = new ParkingGarage(10);
//         garage.InitializeParkingSpaces();

//         foreach(var space in garage.ParkingSpaces)
//         {
//             if (space.IsEmpty)
//                 continue;
//             else
//                 return false;
//         }

//         return true;
//     });




// ----------------------
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