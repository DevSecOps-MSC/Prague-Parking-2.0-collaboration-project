using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class RegisteredVehicleTests
{
    public static void AddTests(object Configuration)
    {
        SimpleTest.AddNewTest(
            new Test("A Registered Vehicle has a vehicle registration", () =>
            {
                var registration = "FOO 666";
                var vehicle = new RegisteredVehicle(registration);

                return vehicle.Registration == registration;
            }
        ));


    }

}
