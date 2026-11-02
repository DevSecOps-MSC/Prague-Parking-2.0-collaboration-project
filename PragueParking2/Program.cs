using FileHandlerLibrary;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;
using Spectre.Console;
internal class Program
{
    static readonly FileHandler fileHandler = new(Admin.path);
    static Config config;
    static ParkingGarage parkingGarage;
    static WorkLog garageLog = new();

    static void PrintLog()
    {
        foreach (var entry in garageLog.AllEntries)
        {
            Console.WriteLine($"{entry.CreatedAt}: {entry.Content}");
        }
    }

    private static void Main(string[] args)
    {
        Console.Clear();

        if (Admin.Setup(fileHandler, garageLog) is false)
        {
            Console.WriteLine("Something went horribly wrong and it appears impossible to set up the software. Exiting.");
            Environment.Exit(666);
        }
        config = Admin.LoadConfig(fileHandler, garageLog);
        parkingGarage = Admin.LoadGarage(fileHandler, config, garageLog);

PrintLog();

        // var bus = new Bus("bus 123", 16);
        // parkingGarage.CheckIn(bus, config.Rates[bus.Type]);
        // parkingGarage.Park(bus);

        // var car = new Car("Car 123", 4);
        // parkingGarage.CheckIn(car, config.Rates[car.Type]);
        // parkingGarage.Park(car, car.Ticket.ParkingSpaceId);

        // var motorcycle = new Motorcycle("Motorcycle 123", 2);
        // parkingGarage.CheckIn(motorcycle, config.Rates[motorcycle.Type]);
        // parkingGarage.Park(motorcycle, motorcycle.Ticket.ParkingSpaceId);

        // var bicycle = new Bicycle("Bicycle 123", 1);
        // parkingGarage.CheckIn(bicycle, config.Rates[bicycle.Type]);
        // parkingGarage.Park(bicycle, bicycle.Ticket.ParkingSpaceId);

        // System.Console.WriteLine(parkingGarage.ToJson2());

        // parkingGarage = ParkingGarage.FromJson2(parkingGarage.ToJson2());

        // System.Console.WriteLine(parkingGarage.ToJson2());

        // ValidatorTests.AddTests(config);
        // VehicleTests.AddTests(config);
        // ValetTicketTests.AddTests(config);
        // ParkingSpaceTests.AddTests(config);
        // ParkingGarageTests.AddTests(config);

        // SimpleTest.RunAllTests();

        // SimpleTestResultReport.WriteReport(SimpleTest.TestResults);


        Menu menu = new(parkingGarage, config);
        List<string> menuItems =
        [
            "Parking",
            "Retrieve",
            "Move",
            "Search",
            "List",
            "Admin",
            "Exit"
        ];

        while (true)
        {
            string choice = menu.Show(menuItems);



            switch (choice)
            {
                case "Parking":
                    AnsiConsole.MarkupLine("[green]Parking selected[/]");
                    break;

                case "Retrieve":
                    AnsiConsole.MarkupLine("[green]Retrieve selected[/]");
                    break;

                case "Move":
                    AnsiConsole.MarkupLine("[green]Move selected[/]");
                    break;

                case "Search":
                    AnsiConsole.MarkupLine("[green]Search selected[/]");
                    break;

                case "List":
                    AnsiConsole.MarkupLine("[green]List selected[/]");
                    break;

                case "Admin":
                    AnsiConsole.MarkupLine("[green]Admin selected[/]");
                    break;

                case "Exit":
                    return;
            }
        }

        // config.SaveToFile(fileHandler, configFilename);
        // parkingGarage.SaveToFile(fileHandler, garageFilename);

    }

}





