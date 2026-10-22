using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public static class ParkingSpaceTests
{
    public static void AddTests(object Configuration)
    {

        SimpleTest.AddNewTest(
            new Test("A Parking Space is empty by default", () =>
            {
                var parkingSpace = new ParkingSpace();

                return parkingSpace.IsEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Space with a parked Vehicle is not empty", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.ParkedVehicles.Add(new Car("_"));

                return parkingSpace.IsEmpty == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Space that is not empty is not empty", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.ParkedVehicles.Add(new Car("_"));

                return parkingSpace.IsNotEmpty == true;
            }
        ));

    }

}
