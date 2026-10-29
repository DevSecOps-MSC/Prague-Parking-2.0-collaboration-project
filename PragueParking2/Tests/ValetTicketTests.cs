using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class ValetTicketTests
{
    public static void AddTests(Config config)
    {
        var testRegistration = Validator.SanitizeVehicleRegistration("foo 666");
        var carSize = config.VehicleSizes[VehicleType.Car];
        var testCar = new Car(testRegistration, carSize);

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket belongs to a Vehicle", () =>
            {
                var ticket = new ValetTicket(testCar, config.Rates);

                return ticket.Vehicle == testCar;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Rate depending on its Type", () =>
            {
                var ticket = new ValetTicket(testCar, config.Rates);

                return ticket.Rate == config.Rates[testCar.Type];
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check In timestamp", () =>
            {
                var expectedTimestamp = DateTime.Now;
                var ticket = new ValetTicket(testCar, config.Rates, expectedTimestamp);

                return ticket.TimeOfCheckIn == expectedTimestamp;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check Out timestamp, initially null", () =>
            {
                var ticket = new ValetTicket(testCar, config.Rates);

                return ticket.TimeOfCheckOut == null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check Out timestamp, when checked out", () =>
            {
                var ticket = new ValetTicket(testCar, config.Rates);
                var expectedTimestamp = DateTime.Now + TimeSpan.FromDays(1);
                ticket.CheckOut(expectedTimestamp);

                return ticket.TimeOfCheckOut == expectedTimestamp;
            }
        ));

    }

}
