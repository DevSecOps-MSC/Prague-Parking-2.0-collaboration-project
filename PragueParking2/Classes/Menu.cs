using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
namespace PragueParking2.Classes
{
    internal class Menu
    {


        private int _selectedIndex;

        public string Show(List<string> items)
        {
            while (true)
            {
                Draw(items);

                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:

                        _selectedIndex =
                            Math.Max(0, _selectedIndex - 1);

                        break;

                    case ConsoleKey.DownArrow:

                        _selectedIndex =
                            Math.Min(items.Count - 1, _selectedIndex + 1);

                        break;

                    case ConsoleKey.Enter:

                        return items[_selectedIndex];
                }
            }
        }

        private void Draw(List<string> items)
        {
            AnsiConsole.Clear();

            string menuItems = string.Join(
                "\n",
                items.Select((item, index) =>
                    index == _selectedIndex
                        ? $"[black on dodgerblue1]> {item}[/]"
                        : $"  {item}")
            );

            var menuPanel = new Panel(menuItems)
                .Header("Menu")
                .Border(BoxBorder.Rounded)
                .Expand();
            // ==========================
            // Map
            // ==========================

            var mapContent = "[white on yellow] ░▒▓█[/]\n " +
                             "[white on darkorange]▓█[/]\n " +
                             "[orangered1 on orangered1]█[/]\n " +
                             "[red3 on red3]████[/]";



            var mapPanel = new Panel(mapContent)
                .Header("Map Of Garage")
                .Border(BoxBorder.Rounded)
                .Expand();
            // ==========================
            // Interactive Table
            // ==========================

            var interactiveTable = new Table()
                .Border(TableBorder.Rounded);

            interactiveTable.AddColumn("[yellow]Field[/]");
            interactiveTable.AddColumn("[yellow]Value[/]");

            interactiveTable.AddRow("Registration", "");
            interactiveTable.AddRow("Vehicle Type", "");


            string currentScreen = "mjau mjau";      // change the hard coded to dynamic with the input from menu

            var interactivePanel = new Panel(interactiveTable)
                .Header(currentScreen)
                .Border(BoxBorder.Rounded)
                .Expand();

            // ==========================
            // Log Table
            // ==========================

            var logTable = new Table()
                .Border(TableBorder.Rounded);

            logTable.AddColumn("[yellow]Event Log[/]");

            logTable.AddRow("[green]Application started[/]");
            logTable.AddRow("Waiting for user input");
            logTable.AddRow(" ");

            var logPanel = new Panel(logTable)
                .Header("Log")
                .Border(BoxBorder.Rounded)
                .Expand();

            // ==========================
            // Layout
            // ==========================

            Layout mainLayout = new Layout("Root")
        .SplitRows(
            new Layout("Top")
            .SplitColumns(
                new Layout("Menu").Size(25),
                new Layout("Map")
            ).Size(20),

            new Layout("Bottom").Size(8)
            .SplitColumns(
                new Layout("Info").Size(55),
                new Layout("Log")
            )
        );

            mainLayout["Menu"].Update(menuPanel);
            mainLayout["Map"].Update(mapPanel);
            mainLayout["Info"].Update(interactivePanel);
            mainLayout["Log"].Update(logPanel);


            AnsiConsole.Write(mainLayout);
        }
    }
}



