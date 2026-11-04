using PragueParking2.Classes;
using SimpleTestLibrary;

namespace PragueParking2.Tests;

public class ValetTicketTests
{
    public static void AddTests(AppConfiguration config)
    {
        var testRegistration = Validator.SanitizeVehicleRegistration("foo 666");
        var carSize = config.VehicleSizes[VehicleType.Car];

        // SimpleTest.AddNewTest(
        //     new Test("Valet Ticket: A Valet Ticket belongs to a Vehicle", () =>
        //     {
        //         var vehicle = new Car(testRegistration, carSize);
        //         var ticket = new ValetTicket(vehicle, config.Rates);

        //         return ticket.Vehicle == vehicle;
        //     }
        // ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Rate depending on its Type", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type]);

                return ticket.Rate == config.Rates[vehicle.Type];
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check In timestamp", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var expectedTimestamp = DateTime.Now;
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type], expectedTimestamp);

                return ticket.TimeOfCheckIn == expectedTimestamp;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check Out timestamp, initially null", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type]);

                return ticket.TimeOfCheckOut == null;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Time of Check Out timestamp, when checked out", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type]);
                var expectedTimestamp = DateTime.Now + TimeSpan.FromDays(1);
                ticket.CheckOut(expectedTimestamp);

                return ticket.TimeOfCheckOut == expectedTimestamp;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket has a Parking Space ID", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type]);

                return ticket.ParkingSpaceId == 0;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: A Valet Ticket's Parking Space ID can be updated", () =>
            {
                var vehicle = new Car(testRegistration, carSize);
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type]);
                var expectedSpaceId = 3;
                ticket.UpdateParkingSpaceId(expectedSpaceId);

                return ticket.ParkingSpaceId == expectedSpaceId;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: Valet Ticket can calculate current Total Time Parked", () =>
            {
                var vehicle = new Car(testRegistration, carSize);

                var expectedTimeInMinutes = 74;
                var ticket = new ValetTicket(
                    vehicle,
                    config.Rates[vehicle.Type],
                    DateTime.Now - TimeSpan.FromMinutes(expectedTimeInMinutes)
                );

                var expectedTotalParkedTime = TimeSpan.FromMinutes(expectedTimeInMinutes);

                return (int)ticket.TotalTimeParked.TotalMinutes == (int)expectedTotalParkedTime.TotalMinutes;
            }
        ));

        SimpleTest.AddNewTest(
            new Test("Valet Ticket: Valet Ticket can calculate final Total Time Parked in minutes", () =>
            {
                var vehicle = new Car(testRegistration, carSize);

                var expectedTimeInMinutes = 74;
                var ticket = new ValetTicket(vehicle, config.Rates[vehicle.Type], DateTime.Now);
                ticket.CheckOut(DateTime.Now + TimeSpan.FromMinutes(expectedTimeInMinutes));

                var expectedTotalParkedTime = TimeSpan.FromMinutes(expectedTimeInMinutes);

                return (int)ticket.TotalTimeParked.TotalMinutes == (int)expectedTotalParkedTime.TotalMinutes;
            }
        ));

    }

}
