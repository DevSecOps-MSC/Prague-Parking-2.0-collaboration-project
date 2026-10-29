using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class ValetTicketTests
{
    public static void AddTests(Config config)
    {
        var testRegistration = Validator.SanitizeVehicleRegistration("foo 666");
        var carSize = config.VehicleSizes[VehicleType.Car];

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket belongs to a Vehicle", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates);

                return ticket.Vehicle == vehicle;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Rate depending on its Type", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates);

                return ticket.Rate == config.Rates[vehicle.Type];
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check In timestamp", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var expectedTimestamp = DateTime.Now;
                var ticket = new ValetTicket(vehicle, config.Rates, expectedTimestamp);

                return ticket.TimeOfCheckIn == expectedTimestamp;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check Out timestamp, initially null", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates);

                return ticket.TimeOfCheckOut == null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check Out timestamp, when checked out", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates);
                var expectedTimestamp = DateTime.Now + TimeSpan.FromDays(1);
                ticket.CheckOut(expectedTimestamp);

                return ticket.TimeOfCheckOut == expectedTimestamp;
            }
        ));

    }

}
