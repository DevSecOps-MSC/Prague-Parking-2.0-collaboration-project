using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class ParkingGarageTests
{
    public static void AddTests(Config config)
    {
        int garageCap = config.ParkingGarageCapacity;
        int spaceCap = config.ParkingSpaceCapacity;
        var testGarage = new ParkingGarage(garageCap, spaceCap);

        SimpleTest.AddNewTest(
            new Test("A Parking Garage has a Capacity", () =>
            {
                var expectedCapacity = 10;
                var parkingGarage = new ParkingGarage(expectedCapacity, spaceCap);

                return parkingGarage.Capacity == expectedCapacity;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A new Parking Garage has a number of Empty Parking Spaces corresponding to its Capacity", () =>
            {
                var expectedCapacity = 10;
                var parkingGarage = new ParkingGarage(expectedCapacity, spaceCap);

                foreach (var parkingSpace in parkingGarage.ParkingSpaces)
                {
                    if (parkingSpace.IsNotEmpty)
                        return false;
                }

                return parkingGarage.ParkingSpaces.Count == expectedCapacity;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Garage can be Initialized with Empty Parking Spaces", () =>
            {
                var expectedCapacity = 10;
                var parkingGarage = new ParkingGarage(expectedCapacity, spaceCap);
                parkingGarage.ParkingSpaces.First().Park(new Car("_", 4));

                parkingGarage.InitializeParkingSpaces(spaceCap);

                foreach (var parkingSpace in parkingGarage.ParkingSpaces)
                {
                    if (parkingSpace.IsNotEmpty)
                        return false;
                }

                return parkingGarage.ParkingSpaces.Count == expectedCapacity;
            }
        ));

        // SimpleTest.AddNewTest(
        //     new Test("A Parking Garage can find the first available Empty Parking Space", () =>
        //     {
        //         var expectedCapacity = 10;
        //         var parkingGarage = new ParkingGarage(expectedCapacity);

        //         return parkingGarage.Capacity == expectedCapacity;
        //     }
        // ));

    }
}
