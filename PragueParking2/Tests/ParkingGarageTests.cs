using PragueParking2.Classes;
using PragueParking2.Classes.Exceptions;
using PragueParking2.Interfaces;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class ParkingGarageTests
{
    public static void AddTests(Config config)
    {
        int garageCap = config.ParkingGarageCapacity;
        int spaceCap = config.ParkingSpaceCapacity;
        var testGarage = new ParkingGarage(garageCap, spaceCap);
        var testCar = new Car("_", config.VehicleSizes[VehicleType.Car]);
        var testMotorcycle = new Motorcycle("_", config.VehicleSizes[VehicleType.Motorcycle]);

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

        SimpleTest.AddNewTest(
            new Test("A Parking Garage has Parking Spaces numbered 1 to [Capacity]", () =>
            {
                var capacity = 10;
                var parkingGarage = new ParkingGarage(capacity, spaceCap);

                return parkingGarage.ParkingSpaces.First().ID == 1
                    && parkingGarage.ParkingSpaces.Last().ID == capacity;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Garage can access every Parking Space by ID", () =>
            {
                var parkingGarage = new ParkingGarage(10, spaceCap);
                var parkingSpace = parkingGarage[3];
                var spaceId = 3;

                return parkingSpace.ID == spaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Garage can find the first available Empty Parking Space", () =>
            {
                var parkingGarage = new ParkingGarage(10, spaceCap);
                parkingGarage.ParkingSpaces.First().Park(testMotorcycle);
                var expectedSpaceId = 2;

                return parkingGarage.GetIdOfFirstEmptySpace() == expectedSpaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Garage can find the first available Parking Space with enough room", () =>
            {
                var size = 4;
                var parkingGarage = new ParkingGarage(10, size);
                var car = new Car("C", size);
                var motorcycleOne = new Motorcycle("M1", size / 2);
                var motorcycleTwo = new Motorcycle("M2", size / 2);

                parkingGarage.ParkingSpaces[0].Park(car);
                parkingGarage.ParkingSpaces[1].Park(motorcycleOne);

                var expectedSpaceId = 2;

                return parkingGarage.GetIdOfFirstAvailableSpace(motorcycleTwo) == expectedSpaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Parking Garage can find a Vehicle", () =>
            {
                var parkingGarage = new ParkingGarage(10, 4);
                parkingGarage[1].Park(testCar);
                parkingGarage[2].Park(testMotorcycle);
                parkingGarage[4].Park(testMotorcycle);
                parkingGarage[5].Park(testMotorcycle);
                parkingGarage[5].Park(testMotorcycle);

                var spaceId = 7;
                var car = new Car("foo 666", 4);
                parkingGarage[spaceId].Park(car);

                return parkingGarage.FindVehicle(car.Registration, out int foundSpaceId) == true
                    && foundSpaceId == spaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Moving a Vehicle from one Parking Space to another, the Vehicle is no longer in the first space", () =>
            {
                IVehicle vehicle = testCar;
                var parkingGarage = new ParkingGarage(10, spaceCap);
                var fromParkingSpaceId = 1;
                var toParkingSpaceId = 3;
                parkingGarage[fromParkingSpaceId].Park(vehicle);

                parkingGarage.Move(vehicle, fromParkingSpaceId, toParkingSpaceId);

                return parkingGarage[fromParkingSpaceId].Contains(vehicle) == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("After Moving a Vehicle from one Parking Space to another, the Vehicle is now in the new space", () =>
            {
                IVehicle vehicle = testCar;
                var parkingGarage = new ParkingGarage(10, spaceCap);
                var fromParkingSpaceId = 1;
                var toParkingSpaceId = 3;
                parkingGarage[fromParkingSpaceId].Park(vehicle);

                parkingGarage.Move(vehicle, fromParkingSpaceId, toParkingSpaceId);

                return parkingGarage[toParkingSpaceId].Contains(vehicle);
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Vehicle can be Parked in a Parking Space with enough room", () =>
            {
                var size = 4;
                IVehicle vehicle = new Vehicle("foo", size);
                var parkingGarage = new ParkingGarage(10, size);
                var spaceId = 1;

                return parkingGarage.Park(vehicle, spaceId);
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Vehicle can not be Parked in a Parking Space without enough room", () =>
            {
                var size = 4;
                IVehicle vehicle = new Vehicle("foo", size + 1);
                var parkingGarage = new ParkingGarage(10, size);
                var spaceId = 1;

                return parkingGarage.Park(vehicle, spaceId) == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Vehicle can not be Parked if its Registration is already in the system", () =>
            {
                var registration = "FOO 666";
                var size = 4;
                IVehicle vehicleOne = new Vehicle(registration, size);
                IVehicle vehicleTwo = new Vehicle(registration, size);
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.Park(vehicleOne, 10);
                var spaceId = 2;

                try
                {
                    parkingGarage.Park(vehicleTwo, spaceId);
                }
                catch (DuplicateVehicleRegistrationException)
                {
                    return true;
                }

                return false;
            }
        ));

    }
}
