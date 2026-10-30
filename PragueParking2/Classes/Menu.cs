using PragueParking2.Classes;
using Spectre.Console;

namespace PragueParking2.Classes;

internal class Menu
{
    private string _registration = "";

    private bool _inputRegistration = false;
    private bool _selectVehicleType = false;

    private VehicleType _selectedVehicleType = VehicleType.Car;
    private int _vehicleTypeIndex = 0;

    private int _selectedIndex;

    private string _currentScreen = "Home";

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

            if (_selectVehicleType)
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

                        _selectVehicleType = false;
                        _inputRegistration = true;

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

            if (_inputRegistration)
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
                    _inputRegistration = false;
     
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

                    if (_currentScreen == "Parking")
                    {
                        _registration = "";

                        _vehicleTypeIndex = 0;

                        _selectVehicleType = true;
                        _inputRegistration = false;
                    }

                    break;
            }
        }
    }
    // reset method
    private void ResetToHome()
    {
        _currentScreen = "Home";

        _selectVehicleType = false;
        _inputRegistration = false;

        _registration = "";
        _vehicleTypeIndex = 0;
    }
    // rendering the menu
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

        // Menu Panel
        var menuPanel = new Panel(menuItems)
            .Header("Menu")
            .Border(BoxBorder.Rounded)
            .Expand();

        // Map Panel
        var mapPanel = new Panel(
            "[white on yellow] ░▒▓█[/]\n" +
            "[white on darkorange]▓█[/]\n" +
            "[orangered1 on orangered1]█[/]\n" +
            "[red3 on red3]████[/]")
            .Header("Map Of Garage")
            .Border(BoxBorder.Rounded)
            .Expand();

        // ===========================
        // Info Panel Content
        // ===========================

        string infoText = "";

        if (_currentScreen == "Home")
        {
            infoText = "";
        }
        else if (_currentScreen == "Parking")
        {
            if (_selectVehicleType)
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
            else if (_inputRegistration)
            {
                infoText =
                    $"Vehicle Type: [yellow]{_selectedVehicleType}[/]\n\n" +
                    $"Registration: [blue]{_registration}█[/]\n\n" +
                    "[grey]ENTER = Save[/]";
            }
        }

        // Interactive Panel
        var interactivePanel =
            new Panel(new Markup(infoText))
                .Header(_currentScreen)
                .Border(BoxBorder.Rounded)
                .Expand();

        // Log Panel
        var logPanel =
            new Panel(
                "[green]Application started[/]")
            .Header("Log")
            .Border(BoxBorder.Rounded)
            .Expand();

        // Layout
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