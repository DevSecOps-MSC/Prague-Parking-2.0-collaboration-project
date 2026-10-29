using FileHandlerLibrary;
using PragueParking2;
using PragueParking2.Classes;
using PragueParking2.Tests;
using SimpleTestLibrary;
using Spectre.Console;
internal class Program
{
    static readonly FileHandler fileHandler = new(Admin.path);
    static Config? config;
    static ParkingGarage? parkingGarage;

    private static void Main(string[] args)
    {
        if (Admin.Setup(fileHandler) is false)
        {
            Console.WriteLine("Something horrible went wrong and it appears impossible to set up the software. Exiting.");
            Environment.Exit(666);
        }
        config = Admin.LoadConfig(fileHandler);
        parkingGarage = Admin.LoadGarage(fileHandler,config);


        // config.SaveToFile(fileHandler, configFilename);
        // parkingGarage.SaveToFile(fileHandler, garageFilename);

 
        ValidatorTests.AddTests(config);
        VehicleTests.AddTests(config);
        ValetTicketTests.AddTests(config);
        ParkingSpaceTests.AddTests(config);
        ParkingGarageTests.AddTests(config);

        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);

   

    Menu menu = new();

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
            AnsiConsole.MarkupLine("[green]Parking screen[/]");
            Console.ReadKey();
            break;

        case "Retrieve":
            AnsiConsole.MarkupLine("[green]Retrieve screen[/]");
            Console.ReadKey();
            break;

        case "Exit":
            return;
    }
}
}

}