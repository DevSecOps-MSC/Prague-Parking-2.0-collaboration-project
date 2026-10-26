using System;
using Spectre.Console;
using PragueParking2.Classes;



#region

string[] choices = [
    "Park vehicle           ",
    "Retrieve vehicle       ",
    "Move vehicle           ",
    "Show parked vehicle    ",
    "Search for vehicle     ",
    "Map of garage          ",
    "List of parked vehicles",
    "Exit"
    ];
int selected = 0;

Panel CreateMenu()
{
    var lines = choices.Select((choice, index) =>
        index == selected
            ? $"[black on cyan] ❯ {Markup.Escape(choice)} [/]"
            : $"  {Markup.Escape(choice)}");

    return new Panel(string.Join("\n", lines))
        .Header("Main menu")
        .BorderColor(Color.Cyan1)
        .Padding(1, 1);
}

AnsiConsole.Live(CreateMenu()).Start(context =>
{
    context.Refresh();

    while (true)
    {
        var key = Console.ReadKey(intercept: true).Key;

        if (key == ConsoleKey.UpArrow)
            selected = (selected - 1 + choices.Length) % choices.Length;
        else if (key == ConsoleKey.DownArrow)
            selected = (selected + 1) % choices.Length;
        else if (key == ConsoleKey.Enter)
            break;
        else
            continue;

        context.UpdateTarget(CreateMenu());
    }
});

AnsiConsole.WriteLine($"You chose: {choices[selected]}");
#endregion


#region Exit program

string exitProgram = AnsiConsole.Prompt(
    new TextPrompt<string>("Do you want to exit program?")
    .AddChoice("Yes")
    .AddChoice("No")
    .DefaultValue("No")
    );

#endregion


