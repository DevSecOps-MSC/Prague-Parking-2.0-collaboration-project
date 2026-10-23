using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public static class ParkingSpaceTests
{
    public static void AddTests(Config config)
    {
        int carSize = config.VehicleSizes[VehicleType.Car];
        Car testCar = new("_", carSize);

        SimpleTest.AddNewTest(
            new Test("A Parking Space Is Empty by default", () =>
            {
                var parkingSpace = new ParkingSpace();

                return parkingSpace.IsEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Space with a Parked Vehicle Is Not Empty", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.ParkedVehicles.Add(testCar);

                return parkingSpace.IsEmpty == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Space that is not Empty Is Not Empty", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.ParkedVehicles.Add(testCar);

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

                return parkingSpace.Park(testCar) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Parking a Vehicle in a Parking Space, the Parking Space is not empty", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.Park(testCar);

                return parkingSpace.IsNotEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Parking a Vehicle in a Parking Space, the Parking Space Contains the Parked Vehicle", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.Park(testCar);

                return parkingSpace.Contains(testCar) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Vehicle can be Collected from a Parking Space", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.Park(testCar);

                return parkingSpace.Collect(testCar) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Collecting a Vehicle from a Parking Space, the Parking Space does not Contain the Vehicle", () =>
            {
                var parkingSpace = new ParkingSpace();
                parkingSpace.Park(testCar);
                parkingSpace.Collect(testCar);

                return parkingSpace.Contains(testCar) == false;
            }
        ));

    }

}
