using PragueParking2.Classes;
internal class Program
{

    private static void Main(string[] args)
    {
        Console.Clear();

        Core core = Admin.SetupApp();
        core.SaveConfiguration();
        core.SaveGarageData();

        core.Log.DumpToConsole();

        Console.WriteLine(core.Config.ToJson());
        Console.WriteLine(core.ParkingGarage.ToJson());



        //     log.AddEntry("Data directory not found. Attempting to load default configuration.");
        //     try
        //     {
        //         config = Config.FromFile(fileHandler, $"{Constants.SourceDirectory}{Constants.DefaultConfigFile}");
        //         log.AddEntry("Default configuration loaded. Proceeding with creation of data directory.");
        //     }
        //     catch (Exception)
        //     {
        //         log.AddEntry("Could not load default configuration.");
        //     }

        //     DirectoryInfo di;
        //     try
        //     {
        //         di = Directory.CreateDirectory(dataDirectory);
        //         log.AddEntry($"Directory [{dataDirectory}] was created successfully! Proceeding with creation of configuration file.");
        //     }
        //     catch (UnauthorizedAccessException)
        //     {
        //         log.AddEntry($"It appears the the user does not have the required permission to create directory [{dataDirectory}]. Functionality of the software will be severely limited.");
        //     }

        // }


        // Environment.Exit(0);


        // config = Admin.LoadConfig(fileHandler, log);
        // parkingGarage = Admin.LoadGarage(fileHandler, config, log);


        // // var bus = new Bus("bus 123", 16);
        // // parkingGarage.CheckIn(bus, config.Rates[bus.Type]);
        // // parkingGarage.Park(bus);

        // // var car = new Car("Car 123", 4);
        // // parkingGarage.CheckIn(car, config.Rates[car.Type]);
        // // parkingGarage.Park(car, car.Ticket.ParkingSpaceId);

        // // var motorcycle = new Motorcycle("Motorcycle 123", 2);
        // // parkingGarage.CheckIn(motorcycle, config.Rates[motorcycle.Type]);
        // // parkingGarage.Park(motorcycle, motorcycle.Ticket.ParkingSpaceId);

        // // var bicycle = new Bicycle("Bicycle 123", 1);
        // // parkingGarage.CheckIn(bicycle, config.Rates[bicycle.Type]);
        // // parkingGarage.Park(bicycle, bicycle.Ticket.ParkingSpaceId);

        // // System.Console.WriteLine(parkingGarage.ToJson2());

        // // parkingGarage = ParkingGarage.FromJson2(parkingGarage.ToJson2());

        // // System.Console.WriteLine(parkingGarage.ToJson2());

        // // ValidatorTests.AddTests(config);
        // // VehicleTests.AddTests(config);
        // // ValetTicketTests.AddTests(config);
        // // ParkingSpaceTests.AddTests(config);
        // // ParkingGarageTests.AddTests(config);

        // // SimpleTest.RunAllTests();

        // // SimpleTestResultReport.WriteReport(SimpleTest.TestResults);


        // Menu menu = new(parkingGarage, config);
        // List<string> menuItems =
        // [
        //     "Parking",
        //     "Retrieve",
        //     "Move",
        //     "Search",
        //     "List",
        //     "Admin",
        //     "Exit"
        // ];

        // while (true)
        // {
        //     string choice = menu.Show(menuItems);



        //     switch (choice)
        //     {
        //         case "Parking":
        //             AnsiConsole.MarkupLine("[green]Parking selected[/]");
        //             break;

        //         case "Retrieve":
        //             AnsiConsole.MarkupLine("[green]Retrieve selected[/]");
        //             break;

        //         case "Move":
        //             AnsiConsole.MarkupLine("[green]Move selected[/]");
        //             break;

        //         case "Search":
        //             AnsiConsole.MarkupLine("[green]Search selected[/]");
        //             break;

        //         case "List":
        //             AnsiConsole.MarkupLine("[green]List selected[/]");
        //             break;

        //         case "Admin":
        //             AnsiConsole.MarkupLine("[green]Admin selected[/]");
        //             break;

        //         case "Exit":
        //             return;
        //     }
        // }

        // config.SaveToFile(fileHandler, configFilename);
        // parkingGarage.SaveToFile(fileHandler, garageFilename);

    }

}





