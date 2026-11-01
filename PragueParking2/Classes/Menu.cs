/*
 TODO
 - Look for 'dead code' and remove
 - Connect SaveConfirmation to SaveToFile()
 - Connect LoadConfirmation to FromFile()
 - Implement ConfigMenu
 - Connect workflow to backend
 - Check naming standard
 - Improve color theme and polish
*/

using Spectre.Console;
using Spectre.Console.Rendering;

namespace PragueParking2.Classes;


  internal enum MenuMode
{
    MainMenu,
    VehicleTypeSelection,
    ParkingRegistration,
    RetrieveRegistration,
    MoveRegistration,
    MoveFromSpace,
    MoveToSpace,
    SearchRegistration,
    ListView,
    AdminMenu,
    SaveConfirmation,
    LoadConfirmation,
    ConfigMenu
}


internal class Menu
{
    private readonly ParkingGarage _garage;
    public Menu(ParkingGarage garage)
    {
        _garage = garage;
    }


    // Fields
    private string _registration = "";
    private string _fromParkingSpace = "";
    private string _toParkingSpace = "";


    // UI state
    private MenuMode _mode = MenuMode.MainMenu;
    private string _currentScreen = "Home";
    private bool _confirmYes = false;


    // Selections
    private int _selectedIndex;
    private int _adminIndex;
    private int _vehicleTypeIndex;
    private VehicleType _selectedVehicleType = VehicleType.Car;



    public string Registration => _registration;
    public VehicleType SelectedVehicleType => _selectedVehicleType;

    private readonly List<string> _logEntries = [];


    // Menu data
    private readonly List<string> _adminItems =
[
    "Save",
    "Load",
    "Config"
];

    // ===========================
    // Log
    // ===========================
    public void AddLog(string message)
    {
        _logEntries.Insert(0, message);

        if (_logEntries.Count > 8)
        {
            _logEntries.RemoveAt(_logEntries.Count - 1);
        }
    }

    // ===========================
    // Main application loop and menu navigation
    // ===========================
    public string Show(List<string> items)
    {
        while (true)
        {
            Draw(items);

            ConsoleKeyInfo keyInfo =
                Console.ReadKey(true);
            if (_mode == MenuMode.ListView)
            {
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ResetToHome();
                }

                continue;
            }


            // ===========================
            // Admin Menu Navigation
            // ===========================
            if (_mode == MenuMode.SaveConfirmation)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:

                        _confirmYes =
                            !_confirmYes;

                        break;

                    case ConsoleKey.Escape:

                        _mode =
                            MenuMode.AdminMenu;

                        break;

                    case ConsoleKey.Enter:

                        if (_confirmYes)
                        {
                            AddLog("Save confirmed");
                        }
                        else
                        {
                            AddLog("Save cancelled");
                        }

                        _mode =
                            MenuMode.AdminMenu;

                        break;
                }

                continue;
            }
            if (_mode == MenuMode.LoadConfirmation)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:

                        _confirmYes =
                            !_confirmYes;

                        break;

                    case ConsoleKey.Escape:

                        _mode =
                            MenuMode.AdminMenu;

                        break;

                    case ConsoleKey.Enter:

                        if (_confirmYes)
                        {
                            AddLog("Load confirmed");
                        }
                        else
                        {
                            AddLog("Load cancelled");
                        }

                        _mode =
                            MenuMode.AdminMenu;

                        break;
                }

                continue;
            }
            if (_mode == MenuMode.AdminMenu)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:

                        _adminIndex =
                            Math.Max(
                                0,
                                _adminIndex - 1);

                        break;

                    case ConsoleKey.DownArrow:

                        _adminIndex =
                            Math.Min(
                                _adminItems.Count - 1,
                                _adminIndex + 1);

                        break;

                    case ConsoleKey.Escape:

                        ResetToHome();

                        break;

                    case ConsoleKey.Enter:

                        string selected =
                            _adminItems[_adminIndex];

                        if (selected == "Save")
                        {
                            _confirmYes = false;
                            _mode = MenuMode.SaveConfirmation;
                        }
                        else if (selected == "Load")
                        {
                            _confirmYes = false;
                            _mode = MenuMode.LoadConfirmation;
                        }
                        else if (selected == "Config")
                        {
                            _mode = MenuMode.ConfigMenu;
                        }

                        break;
                }

                continue;
            }


            // ===========================
            // Vehicle Type Selection
            // ===========================
            if (_mode == MenuMode.VehicleTypeSelection)
            {
                int maxIndex =
                    Enum.GetValues<VehicleType>().Length - 1;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:

                        _vehicleTypeIndex =
                            Math.Max(
                                0,
                                _vehicleTypeIndex - 1);

                        break;

                    case ConsoleKey.DownArrow:

                        _vehicleTypeIndex =
                            Math.Min(
                                maxIndex,
                                _vehicleTypeIndex + 1);

                        break;

                    case ConsoleKey.Enter:

                        _selectedVehicleType =
                            Enum.GetValues<VehicleType>()
                                [_vehicleTypeIndex];

                        _mode = MenuMode.ParkingRegistration;

                        break;

                    case ConsoleKey.Escape:

                        ResetToHome();

                        break;
                }

                continue;
            }


            // ===========================
            // Registration Input
            // ===========================
            if
            (
            _mode == MenuMode.ParkingRegistration ||
            _mode == MenuMode.RetrieveRegistration ||
            _mode == MenuMode.MoveRegistration ||
            _mode == MenuMode.SearchRegistration
            )
            {
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (_registration.Length > 0)
                    {
                        _registration =
                            _registration.Substring(
                                0,
                                _registration.Length - 1);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (_mode == MenuMode.RetrieveRegistration)
                    {
                        if (_garage.FindVehicle(
                            _registration,
                            out int spaceId))
                        {
                            AddLog(
                                $"Found {_registration} in space #{spaceId}");
                        }
                        else
                        {
                            AddLog(
                                $"Vehicle {_registration} not found");
                        }
                    }
                    if (_mode == MenuMode.MoveRegistration)
                    {
                        _fromParkingSpace = "";

                        _mode = MenuMode.MoveFromSpace;

                        continue;
                    }
                    _mode = MenuMode.MainMenu;
                    _currentScreen = "Home";
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ResetToHome();
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    _registration +=
                        char.ToUpper(keyInfo.KeyChar);
                }

                continue;
            }



            // ===========================
            // Move From Space Input
            // ===========================
            if (_mode == MenuMode.MoveFromSpace)
            {
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (_fromParkingSpace.Length > 0)
                    {
                        _fromParkingSpace =
                            _fromParkingSpace[..^1];
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    _toParkingSpace = "";

                    _mode = MenuMode.MoveToSpace;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ResetToHome();
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    _fromParkingSpace += keyInfo.KeyChar;
                }

                continue;
            }

            // ===========================
            // Move To Space Input
            // ===========================
            if (_mode == MenuMode.MoveToSpace)
            {
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (_toParkingSpace.Length > 0)
                    {
                        _toParkingSpace =
                            _toParkingSpace[..^1];
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    AddLog(
                        $"Move {_registration} from {_fromParkingSpace} to {_toParkingSpace}");

                    ResetToHome();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    ResetToHome();
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    _toParkingSpace += keyInfo.KeyChar;
                }

                continue;
            }


            // ===========================
            // Main Menu
            // ===========================
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:

                    _selectedIndex =
                        Math.Max(
                            0,
                            _selectedIndex - 1);

                    break;

                case ConsoleKey.DownArrow:

                    _selectedIndex =
                        Math.Min(
                            items.Count - 1,
                            _selectedIndex + 1);

                    break;

                case ConsoleKey.Enter:

                    _currentScreen =
                        items[_selectedIndex];

                    if (_currentScreen == "Exit")
                    {
                        return "Exit";
                    }

                    switch (_currentScreen)
                    {
                        case "Parking":

                            _registration = "";
                            _vehicleTypeIndex = 0;

                            _mode =
                                MenuMode.VehicleTypeSelection;

                            break;

                        case "Retrieve":

                            _registration = "";

                            _mode =
                                MenuMode.RetrieveRegistration;

                            break;


                        case "Move":

                            _registration = "";

                            _mode =
                                MenuMode.MoveRegistration;

                            break;


                        case "Search":

                            _registration = "";

                            _mode =
                                MenuMode.SearchRegistration;

                            break;


                        case "Admin":

                            _adminIndex = 0;

                            _mode =
                                MenuMode.AdminMenu;

                            break;


                        case "List":
                            _mode =
                                MenuMode.ListView;
                            break;
                    }

                    break;
            }
        }
    }
          
        
  
    private void ResetToHome()
    {
        _currentScreen = "Home";

        _mode = MenuMode.MainMenu;

        _registration = "";
        _vehicleTypeIndex = 0;
    }



    // ===========================
    // UI Rendering
    // ===========================
    private void Draw(List<string> items)
    {
        AnsiConsole.Clear();

        if (_mode == MenuMode.ListView)
        {
            DrawListView();
            return;
        }

        string menuItems =
            string.Join(
                "\n",
                items.Select((item, index) =>
                    index == _selectedIndex
                        ? $"[black on dodgerblue1]> {item}[/]"
                        : $"  {item}"));

        var menuPanel = new Panel(menuItems)
            .Header("Menu")
            .Border(BoxBorder.Rounded)
            .Expand();

        var mapPanel = new Panel(GarageMap.Build(_garage))
            .Header("Map Of Garage")
            .Border(BoxBorder.Rounded)
            .Expand();

        string infoText = "";

        IRenderable panelContent =
            new Markup("");

        if (_currentScreen == "Home")
        {
            infoText = "";
        }
        else 
        {
            if 
                (
                 _currentScreen == "Parking" &&
                 _mode == MenuMode.VehicleTypeSelection
                )
            {
                var vehicleTypes =
                    Enum.GetValues<VehicleType>();

                string typeMenu =
                    string.Join(
                        "\n",
                        vehicleTypes.Select((type, index) =>
                            index == _vehicleTypeIndex
                                ? $"> [green]{type}[/]"
                                : $"  {type}"
                        ));

                infoText =
                    "[yellow]Select Vehicle Type[/]\n\n" +
                    typeMenu;
            }
            else if
            (
            _mode == MenuMode.ParkingRegistration ||
            _mode == MenuMode.RetrieveRegistration ||
            _mode == MenuMode.MoveRegistration ||
            _mode == MenuMode.MoveFromSpace ||
            _mode == MenuMode.MoveToSpace ||
            _mode == MenuMode.SearchRegistration
            )
            {
                if (_mode == MenuMode.ParkingRegistration)
                {
                    infoText =
                        $"Vehicle Type: [yellow]{_selectedVehicleType}[/]\n\n" +
                        $"Registration: [blue]{_registration}█[/]\n\n" +
                        "[grey]ENTER = Save[/]";
                }
                else if (_mode == MenuMode.RetrieveRegistration)
                {
                    infoText =
                        "[yellow]Retrieve Vehicle[/]\n\n" +
                        $"Registration: [blue]{_registration}█[/]\n\n" +
                        "[grey]ENTER = Search[/]";
                }
                else if (_mode == MenuMode.MoveRegistration)
                {
                    infoText =
                        "[yellow]Move Vehicle[/]\n\n" +
                        $"Registration: [blue]{_registration}█[/]\n\n" +
                        "[grey]ENTER = Continue[/]";
                }
                else if (_mode == MenuMode.MoveFromSpace)
                {
                    infoText =
                        "[yellow]Move Vehicle[/]\n\n" +
                        $"Registration: [green]{_registration}[/]\n\n" +
                        $"From Space: [blue]{_fromParkingSpace}█[/]";
                }
                else if (_mode == MenuMode.MoveToSpace)
                {
                    infoText =
                        "[yellow]Move Vehicle[/]\n\n" +
                        $"Registration: [green]{_registration}[/]\n\n" +
                        $"From Space: [green]{_fromParkingSpace}[/]\n\n" +
                        $"To Space: [blue]{_toParkingSpace}█[/]";
                }
                else if (_mode == MenuMode.SearchRegistration)
                {
                    infoText =
                        "[yellow]Search Vehicle[/]\n\n" +
                        $"Registration: [blue]{_registration}█[/]\n\n" +
                        "[grey]ENTER = Search[/]";
                }
            }
            else if (_mode == MenuMode.AdminMenu)
            {
                string adminMenu =
                    string.Join(
                        "\n",
                        _adminItems.Select(
                            (item, index) =>
                                index == _adminIndex
                                    ? $"> [green]{item}[/]"
                                    : $"  {item}"
                        ));

                infoText =
                    "[yellow]Admin[/]\n\n" +
                    adminMenu +
                    "\n\n[grey]ENTER = Select[/]" +
                    "\n[grey]ESC = Back[/]";
            }
            else if (_mode == MenuMode.SaveConfirmation)
            {
                infoText =
                    "[yellow]Do you want to save?[/]\n\n";

                if (_confirmYes)
                {
                    infoText +=
                        "  No\n" +
                        "> [green]Yes[/]";
                }
                else
                {
                    infoText +=
                        "> [red]No[/]\n" +
                        "  Yes";
                }

                infoText +=
                    "\n\n[grey]ENTER = Confirm[/]" +
                    "\n[grey]ESC = Cancel[/]";
            }
            else if (_mode == MenuMode.LoadConfirmation)
            {
                infoText =
                    "[yellow]Do you want to load?[/]\n\n";

                if (_confirmYes)
                {
                    infoText +=
                        "  No\n" +
                        "> [green]Yes[/]";
                }
                else
                {
                    infoText +=
                        "> [red]No[/]\n" +
                        "  Yes";
                }

                infoText +=
                    "\n\n[grey]ENTER = Confirm[/]" +
                    "\n[grey]ESC = Cancel[/]";
            }
        }
  
            panelContent =
                new Markup(infoText);
        
        var interactivePanel =
        new Panel(panelContent)
                 .Header(_currentScreen)
                .Border(BoxBorder.Rounded)
                .Expand();

        string logText = string.Join("\n", _logEntries);

        var logPanel =
            new Panel(logText)
                .Header("Log")
                .Border(BoxBorder.Rounded)
                .Expand();



        // ===========================
        // Layout
        // ===========================
        Layout mainLayout = new Layout("Root")
            .SplitRows(
                new Layout("Top")
                    .SplitColumns(
                        new Layout("Menu").Size(25),
                        new Layout("Map"))
                    .Size(20),

                new Layout("Bottom")
                    .Size(9)
                    .SplitColumns(
                        new Layout("Info").Size(55),
                        new Layout("Log"))
            );

        mainLayout["Menu"].Update(menuPanel);
        mainLayout["Map"].Update(mapPanel);
        mainLayout["Info"].Update(interactivePanel);
        mainLayout["Log"].Update(logPanel);

        AnsiConsole.Write(mainLayout);
    }
    
    //List Table 
    private void DrawListView()
    {
        var table = new Table();

        table.Border(TableBorder.Rounded);

        table.AddColumn("Registration");
        table.AddColumn("Space");
        table.AddColumn("Check In");
        table.AddColumn("Price");

        table.AddRow(
            "ABC123",
            "4",
            "16:30",
            "25 kr");

        table.AddRow(
            "XYZ789",
            "7",
            "17:05",
            "10 kr");
        var panel =
    new Panel(table)
        .Header("Registered Vehicles")
        .Border(BoxBorder.Rounded)
        .Expand();

        AnsiConsole.Write(panel);

        AnsiConsole.MarkupLine(
            "\n[grey]ESC = Back[/]");
    }
    
}
