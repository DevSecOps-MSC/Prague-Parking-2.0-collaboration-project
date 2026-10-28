using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class VehicleTests
{
    public static void AddTests(object Configuration)
    {
        var testRegistration = Validator.SanitizeVehicleRegistration("foo 666");

        SimpleTest.AddNewTest(
            new Test("A Vehicle has a vehicle Registration", () =>
            {
                var vehicle = new RegisteredVehicle(testRegistration);

                return vehicle.Registration == testRegistration;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("A Vehicle has a Size", () =>
            {
                var size = 4;
                var vehicle = new RegisteredVehicle(testRegistration, size);

                return vehicle.Size == size;
            }
        ));


    }

}
