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

        SimpleTest.AddNewTest(
            new Test("A Parking Space has a List of Parked Vehicles", () =>
            {
                var parkingSpace = new ParkingSpace();

                return parkingSpace.ParkedVehicles is not null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Vehicle can be Parked in a Parking Space", () =>
            {
                var parkingSpace = new ParkingSpace();

                return parkingSpace.Park(new Car("_")) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Parking a Vehicle in a Parking Space, the Parking Space is not empty", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.Park(new Car("_"));

                return parkingSpace.IsNotEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Parking a Vehicle in a Parking Space, the Parking Space Contains the Parked Vehicle", () =>
            {
                var parkingSpace = new ParkingSpace();
                var car = new Car("_");
                parkingSpace.Park(car);

                return parkingSpace.Contains(car) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Vehicle can be Collected from a Parking Space", () =>
            {
                var parkingSpace = new ParkingSpace();
                var car = new Car("_");
                parkingSpace.Park(car);

                return parkingSpace.Collect(car) == true;
            }
        ));

        // SimpleTest.AddNewTest(
        //     new Test("After Collecting a Vehicle from a Parking Space", () =>
        //     {
        //         var parkingSpace = new ParkingSpace();
        //         var car = new Car("_");
        //         parkingSpace.Park(car);

        //         return parkingSpace.Collect(car) == true;
        //     }
        // ));

    }

}
