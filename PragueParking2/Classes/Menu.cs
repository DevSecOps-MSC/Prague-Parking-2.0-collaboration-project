



using PragueParking2.Classes;
using Spectre.Console;

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
    AdminMenu
}


internal class Menu
{
    private readonly ParkingGarage _garage;

    public Menu(ParkingGarage garage)
    {
        _garage = garage;
    }
    private string _fromParkingSpace = "";
    private string _toParkingSpace = "";
    private string _registration = "";
    private MenuMode _mode = MenuMode.MainMenu;

    private VehicleType _selectedVehicleType = VehicleType.Car;
    private int _vehicleTypeIndex = 0;

    private int _selectedIndex;

    private string _currentScreen = "Home";

    public string Registration => _registration;
    public VehicleType SelectedVehicleType => _selectedVehicleType;

    private readonly List<string> _logEntries = [];

    public void AddLog(string message)
    {
        _logEntries.Insert(0, message);

        if (_logEntries.Count > 8)
        {
            _logEntries.RemoveAt(_logEntries.Count - 1);
        }
    }

    public string Show(List<string> items)
    {
        while (true)
        {
            Draw(items);

            ConsoleKeyInfo keyInfo =
                Console.ReadKey(true);

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
                        if (_mode == MenuMode.RetrieveRegistration)                             //retrieve
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
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            _fromParkingSpace += keyInfo.KeyChar;
                        }

                        continue;
                    }
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
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            _toParkingSpace += keyInfo.KeyChar;
                        }

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

                            _mode =
                                MenuMode.AdminMenu;

                            break;

                        case "List":

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

    private void Draw(List<string> items)
    {
        AnsiConsole.Clear();

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

        if (_currentScreen == "Home")
        {
            infoText = "";
        }
        else //if (_currentScreen == "Parking")
        {
            if //(_mode == MenuMode.VehicleTypeSelection)
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
                else if (_mode == MenuMode.AdminMenu)
                {
                    infoText =
                        "[yellow]Admin[/]\n\n" +
                        "Save\n" +
                        "Load\n" +
                        "Statistics\n" +
                        "Reset Garage";
                }
            }
        }

        var interactivePanel =
            new Panel(new Markup(infoText))
                .Header(_currentScreen)
                .Border(BoxBorder.Rounded)
                .Expand();

        string logText = string.Join("\n", _logEntries);

        var logPanel =
            new Panel(logText)
                .Header("Log")
                .Border(BoxBorder.Rounded)
                .Expand();

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
}