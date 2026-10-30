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
        IVehicle testCar = new Car("_", config.VehicleSizes[VehicleType.Car]).SimulateCheckIn(config);
        IVehicle testMotorcycle = new Motorcycle("_", config.VehicleSizes[VehicleType.Motorcycle]).SimulateCheckIn(config);

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Parking Garage has a Capacity", () =>
            {
                var expectedCapacity = 10;
                var parkingGarage = new ParkingGarage(expectedCapacity, spaceCap);

                return parkingGarage.Capacity == expectedCapacity;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A new Parking Garage has a number of Empty Parking Spaces corresponding to its Capacity", () =>
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
            new Test("Parking Garage: A Parking Garage can be Initialized with Empty Parking Spaces", () =>
            {
                var expectedCapacity = 10;
                var parkingGarage = new ParkingGarage(expectedCapacity, spaceCap);
                parkingGarage.ParkingSpaces.First().Park(new Car("_", 4).SimulateCheckIn(config));

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
            new Test("Parking Garage: A Parking Garage has Parking Spaces numbered 1 to [Capacity]", () =>
            {
                var capacity = 10;
                var parkingGarage = new ParkingGarage(capacity, spaceCap);

                return parkingGarage.ParkingSpaces.First().ID == 1
                    && parkingGarage.ParkingSpaces.Last().ID == capacity;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Parking Garage can access every Parking Space by ID", () =>
            {
                var parkingGarage = new ParkingGarage(10, spaceCap);
                var parkingSpace = parkingGarage[3];
                var spaceId = 3;

                return parkingSpace.ID == spaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Parking Garage can find the first available Empty Parking Space", () =>
            {
                var parkingGarage = new ParkingGarage(10, spaceCap);
                parkingGarage.ParkingSpaces.First().Park(testMotorcycle);
                var expectedSpaceId = 2;

                return parkingGarage.GetIdOfFirstEmptySpace() == expectedSpaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Parking Garage can find the first available Parking Space with enough room", () =>
            {
                var size = 4;
                var parkingGarage = new ParkingGarage(10, size);
                var car = new Car("C", size).SimulateCheckIn(config);
                var motorcycleOne = new Motorcycle("M1", size / 2).SimulateCheckIn(config);
                var motorcycleTwo = new Motorcycle("M2", size / 2).SimulateCheckIn(config);

                parkingGarage.ParkingSpaces[0].Park(car);
                parkingGarage.ParkingSpaces[1].Park(motorcycleOne);

                var expectedSpaceId = 2;

                return parkingGarage.GetIdOfFirstAvailableSpace(motorcycleTwo) == expectedSpaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Parking Garage can find a Vehicle", () =>
            {
                var parkingGarage = new ParkingGarage(10, 4);
                parkingGarage[1].Park(testCar);
                parkingGarage[2].Park(testMotorcycle);
                parkingGarage[4].Park(testMotorcycle);
                parkingGarage[5].Park(testMotorcycle);
                parkingGarage[5].Park(testMotorcycle);

                var spaceId = 7;
                var car = new Car("foo 666", 4).SimulateCheckIn(config);
                parkingGarage[spaceId].Park(car);

                return parkingGarage.FindVehicle(car.Registration, out int foundSpaceId) == true
                    && foundSpaceId == spaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: After Moving a Vehicle from one Parking Space to another, the Vehicle is no longer in the first space", () =>
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
            new Test("Parking Garage: After Moving a Vehicle from one Parking Space to another, the Vehicle is now in the new space", () =>
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
                IVehicle vehicle = new Car("foo", size).SimulateCheckIn(config);
                var parkingGarage = new ParkingGarage(10, size);
                var spaceId = 1;

                return parkingGarage.Park(vehicle, spaceId);
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Vehicle can not be Parked in a Parking Space without enough room", () =>
            {
                var size = 4;
                IVehicle vehicle = new Car("foo", size + 1).SimulateCheckIn(config);
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
                IVehicle vehicleOne = new Car(registration, size).SimulateCheckIn(config);
                IVehicle vehicleTwo = new Car(registration, size).SimulateCheckIn(config);
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

        SimpleTest.AddNewTest(
            new Test("Parking Garage: It is possible to Count the number of Empty Parking Spaces", () =>
            {
                var expectedNumber = 10;
                var parkingGarage = new ParkingGarage(expectedNumber, spaceCap);

                return parkingGarage.CountEmptyParkingSpaces() == expectedNumber;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: It is possible to Count the number of Parked Cars", () =>
            {
                var size = 4;
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.Park(new Car("C1", size).SimulateCheckIn(config), 1);
                parkingGarage.Park(new Car("C2", size).SimulateCheckIn(config), 2);
                var expectedNumber = 2;

                return parkingGarage.CountParkedCars() == expectedNumber;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: It is possible to Count the number of Parked Motorcycles", () =>
            {
                var size = 4;
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.Park(new Motorcycle("M1", size / 2).SimulateCheckIn(config), 1);
                parkingGarage.Park(new Motorcycle("M2", size / 2).SimulateCheckIn(config), 2);
                parkingGarage.Park(new Motorcycle("M3", size / 2).SimulateCheckIn(config), 2);
                var expectedNumber = 3;

                return parkingGarage.CountParkedMotorcycles() == expectedNumber;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: It is possible to Count the number of Parked Bicycles", () =>
            {
                var size = 4;
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.Park(new Bicycle("B1", size / 4).SimulateCheckIn(config), 1);
                parkingGarage.Park(new Bicycle("B2", size / 4).SimulateCheckIn(config), 2);
                parkingGarage.Park(new Bicycle("B3", size / 4).SimulateCheckIn(config), 2);
                var expectedNumber = 3;

                return parkingGarage.CountParkedBicycles() == expectedNumber;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Car gets a Valet Ticket when it is Checked In", () =>
            {
                var size = 4;
                IVehicle vehicle = new Car("foo", size);
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.CheckIn(vehicle, config.Rates);

                return vehicle.Ticket is not null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Motorcycle gets a Valet Ticket when it is Checked In", () =>
            {
                var size = 4;
                IVehicle vehicle = new Motorcycle("foo", size / 2);
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.CheckIn(vehicle, config.Rates);

                return vehicle.Ticket is not null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Bus gets a Valet Ticket when it is Checked In", () =>
            {
                var size = 4;
                IVehicle vehicle = new Bus("foo", size * 4);
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.CheckIn(vehicle, config.Rates);

                return vehicle.Ticket is not null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Bicycle gets a Valet Ticket when it is Checked In", () =>
            {
                var size = 4;
                IVehicle vehicle = new Bicycle("foo", size / 4);
                var parkingGarage = new ParkingGarage(10, size);
                parkingGarage.CheckIn(vehicle, config.Rates);

                return vehicle.Ticket is not null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Parking Garage: A Vehicle can only be parked if it has a Valet Ticket", () =>
            {
                var size = 4;
                IVehicle vehicle = new Car("foo", size);
                var parkingGarage = new ParkingGarage(10, size);

                return parkingGarage.Park(vehicle, 1) == false;
            }
        ));



    }
}
