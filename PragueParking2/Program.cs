using PragueParking2.Classes;
using Spectre.Console;
internal class Program
{

    private static void Main(string[] args)
    {
        Console.Clear();

        Core core = Admin.SetupApp(args);
        core.SaveConfiguration();
        core.SaveGarageData();

        core.Log.DumpToConsole();

        Console.WriteLine(core.Config.ToJson());
        Console.WriteLine(core.ParkingGarage.ToJson());

        Menu menu = new(core.ParkingGarage, core.Config);
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





