using FileHandlerLibrary;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;
using Spectre.Console;
internal class Program
{
    static readonly FileHandler fileHandler = new(Admin.path);
    static Config? config;
    static ParkingGarage? parkingGarage;
    static WorkLog? workLog;


    private static void Main(string[] args)
    {
        if (Admin.Setup(fileHandler) is false)
        {
            Console.WriteLine("Something went horribly wrong and it appears impossible to set up the software. Exiting.");
            Environment.Exit(666);
        }
        // config = Admin.LoadConfig(fileHandler);
        config = Admin.LoadDefaultConfig();
        // parkingGarage = Admin.LoadGarage(fileHandler, config);
        parkingGarage = ParkingGarage.FromConfig(config);


        // config.SaveToFile(fileHandler, configFilename);
        // parkingGarage.SaveToFile(fileHandler, garageFilename);


        ValidatorTests.AddTests(config);
        VehicleTests.AddTests(config);
        ValetTicketTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);
        ParkingGarageTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);


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
  
    }

}





