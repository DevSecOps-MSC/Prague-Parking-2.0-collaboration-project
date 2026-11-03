using PragueParking2.Classes;
internal class Program
{

    private static void Main(string[] args)
    {
        Console.Clear();

        Core core = Admin.SetupApp(args);
        core.SaveConfiguration();
        core.SaveGarageData();

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

            if (choice == "Exit")
            {
                core.SaveConfiguration();
                core.SaveGarageData();
                core.Log.DumpToConsole();
                Environment.Exit(0);
            }

        }


    }

}





