using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class ValidatorTests
{
    public static void AddTests(object Configuration)
    {
        SimpleTest.AddNewTest(
            new Test("Validator: A Vehicle Registration can sanitized and cleaned up", () =>
            {
                string testString = " foo 666  ";
                string expectedResult = "FOO 666";

                return Validator.SanitizeVehicleRegistration(testString).Equals(expectedResult);
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Validator: A Vehicle Registration can not be empty", () =>
            {
                string testString = "";

                return Validator.VehicleRegistrationIsValid(testString) == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Validator: A Vehicle Registration can not be more than 10 characters long", () =>
            {
                string testString = "xxx-xxx-xxx";

                return Validator.VehicleRegistrationIsValid(testString) == false;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Validator: A Vehicle Registration can contain more than one space", () =>
            {
                string testString = "x x x";

                return Validator.VehicleRegistrationIsValid(testString) == true;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Validator: A Vehicle Registration can only contain approved characters", () =>
            {
                string[] testStrings = [
                    "xxx . xxx",
                    "foo &",
                    "b*rk",
                    "# glorp",
                    "glorp?",
                    "2 ewq k !",
                    "foo \\ 666",
                    "baz / 23",
                    "boo @ 69",
                ];

                foreach (var testString in testStrings)
                {
                    if (Validator.VehicleRegistrationIsValid(testString))
                        return false;
                }

                return true;
            }
        ));

    }
}
