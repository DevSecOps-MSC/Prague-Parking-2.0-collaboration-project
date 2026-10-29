using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class VehicleTests
{
    public static void AddTests(Config config)
    {
        var testRegistration = Validator.SanitizeVehicleRegistration("foo 666");
        var carSize = config.VehicleSizes[VehicleType.Car];

        SimpleTest.AddNewTest(
            new Test("Vehicle: A Vehicle has a vehicle Registration", () =>
            {
                var vehicle = new Vehicle(testRegistration);

                return vehicle.Registration == testRegistration;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Vehicle: A Vehicle has a Size", () =>
            {
                var size = 4;
                var vehicle = new Vehicle(testRegistration, size);

                return vehicle.Size == size;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Vehicle: A Vehicle has a Vehicle Type", () =>
            {
                var car = new Car(testRegistration, 4);

                return car.Type == VehicleType.Car;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Vehicle: A Vehicle has a Vale Ticket", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates);

                return vehicle.Ticket == ticket;
            }
        ));


    }

}
