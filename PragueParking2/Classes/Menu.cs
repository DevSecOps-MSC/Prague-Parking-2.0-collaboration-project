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

                        if (_selectedIndex > 0)
                            _selectedIndex--;

                        break;

                    case ConsoleKey.DownArrow:

                        if (_selectedIndex < items.Count - 1)
                            _selectedIndex++;

                        break;

                    case ConsoleKey.Enter:

                        return items[_selectedIndex];
                }
            }
        }
        //  code for menu panel only

        /* private void Draw(List<string> items)
         {
             AnsiConsole.Clear();

             string rows = string.Join(                  
                 "\n",
                 items.Select((item, index) =>
                     index == _selectedIndex
                         ? $"[black on dodgerblue1]> {item}[/]"
                         : $"  {item}"));

             var panel = new Panel(rows)
                 .Header("Menu")
                 .Border(BoxBorder.Rounded);

             AnsiConsole.Write(panel);
         }  */
        private void Draw(List<string> items)
        {
            AnsiConsole.Clear();

            string rows = string.Join(
                "\n",
                items.Select((item, index) =>
                    index == _selectedIndex
                        ? $"[black on dodgerblue1]> {item}[/]"
                        : $"  {item}"));

            var menuPanel = new Panel(rows)
                .Header("Menu")
                .Border(BoxBorder.Rounded);

            var mapPanel = new Panel(
            string.Join("\n", Enumerable.Repeat("                                                                     ", 20)))
            .Header("Map of Garage")
            .Border(BoxBorder.Rounded);


            var grid = new Grid();

            grid.AddColumn(new GridColumn().Width(25));
            grid.AddColumn();

            grid.AddRow(menuPanel, mapPanel);

            AnsiConsole.Write(grid);
        }




    }
}


  
