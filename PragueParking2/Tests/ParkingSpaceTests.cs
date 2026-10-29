using PragueParking2.Classes;
using PragueParking2.Classes.Exceptions;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public static class ParkingSpaceTests
{
    public static void AddTests(Config config)
    {
        int carSize = config.VehicleSizes[VehicleType.Car];
        int motorcycleSize = config.VehicleSizes[VehicleType.Motorcycle];
        int spaceCap = config.ParkingSpaceCapacity;
        Bus testCar = new("_", carSize);
        Motorcycle testMotorcycle = new("_", motorcycleSize);

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space Is Empty by default", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);

                return parkingSpace.IsEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space with a Parked Vehicle Is Not Empty", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.ParkedVehicles.Add(testCar);

                return parkingSpace.IsEmpty == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space that is not Empty Is Not Empty", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.ParkedVehicles.Add(testCar);

                return parkingSpace.IsNotEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space has a List of Parked Vehicles", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);

                return parkingSpace.ParkedVehicles is not null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Vehicle can be Parked in a Parking Space", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);

                return parkingSpace.Park(testCar) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: After Parking a Vehicle in a Parking Space, the Parking Space is not empty", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.Park(testCar);

                return parkingSpace.IsNotEmpty == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: After Parking a Vehicle in a Parking Space, the Parking Space Contains the Parked Vehicle", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.Park(testCar);

                return parkingSpace.Contains(testCar) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Vehicle can be Collected from a Parking Space", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.Park(testCar);

                return parkingSpace.Collect(testCar) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: After Collecting a Vehicle from a Parking Space, the Parking Space does not Contain the Vehicle", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.Park(testCar);
                parkingSpace.Collect(testCar);

                return parkingSpace.Contains(testCar) == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: An Empty Parking Space has Available Space equal to its Capacity", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);

                return parkingSpace.AvailableSpace == parkingSpace.Capacity;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A non-empty Parking Space has Available Space equal to its Capacity minus total Sizes of its Parked Vehicles", () =>
            {
                var parkingSpace = new ParkingSpace(spaceCap);
                parkingSpace.Park(testMotorcycle);
                var expectedAvailableSpace = parkingSpace.Capacity - config.VehicleSizes[VehicleType.Motorcycle];

                return parkingSpace.AvailableSpace == expectedAvailableSpace;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space Is Full if it has no Available Space", () =>
            {
                var testSize = 4;
                var parkingSpace = new ParkingSpace(testSize);
                var car = new Car("_", testSize);

                parkingSpace.Park(car);

                return parkingSpace.IsFull == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space can hold more than one Vehicle if there is enough Available Space", () =>
            {
                var testSize = 4;
                var parkingSpace = new ParkingSpace(testSize);

                var motorcycleOne = new Motorcycle("M1", testSize / 2);
                var motorcycleTwo = new Motorcycle("M2", testSize / 2);

                parkingSpace.Park(motorcycleOne);

                return parkingSpace.Park(motorcycleTwo) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Vehicle can only be Parked in a Parking Space if there is enough Available Space", () =>
            {
                var testSize = 4;
                var parkingSpace = new ParkingSpace(testSize);

                var motorcycleOne = new Motorcycle("M1", testSize / 2);
                var motorcycleTwo = new Motorcycle("M2", testSize / 2);
                var motorcycleThree = new Motorcycle("M3", testSize / 2);

                parkingSpace.Park(motorcycleOne);
                parkingSpace.Park(motorcycleTwo);

                try
                {
                    parkingSpace.Park(motorcycleThree);
                }
                catch (NotEnoughRoomException)
                {
                    return true;
                }

                return false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: A Parking Space has an ID", () =>
            {
                var expectedID = 3;
                var parkingSpace = new ParkingSpace(expectedID, spaceCap);

                return parkingSpace.ID == expectedID;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: It is possible to see if a Parking Space Contains Cars", () =>
            {
                var size = 4;
                var parkingSpace = new ParkingSpace(size);
                var car = new Car("C", size);
                parkingSpace.Park(car);

                return parkingSpace.ContainsCars(out int qty) == true
                    && qty == 1;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Space: It is possible to see if a Parking Space Contains Motorcycles", () =>
            {
                var size = 4;
                var parkingSpace = new ParkingSpace(size);
                var motorcycleOne = new Motorcycle("M1", size / 2);
                var motorcycleTwo = new Motorcycle("M2", size / 2);
                parkingSpace.Park(motorcycleOne);
                parkingSpace.Park(motorcycleTwo);

                return parkingSpace.ContainsMotorcycles(out int qty) == true
                    && qty == 2;
            }
        ));


    }

}
