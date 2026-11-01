using System.Text;

namespace PragueParking2.Classes;

internal static class GarageMap
{
    internal static string Build(ParkingGarage garage)
    {
        var map = new StringBuilder();

        int columns = 10;
        int count = 0;

        map.AppendLine(
            "[green]4 Free[/] | " +
            "[yellow]3 Free[/] | " +
            "[darkorange]2 Free[/] | " +
            "[orangered1]1 Free[/] | " +
            "[red]Full[/]");

        map.AppendLine();

        foreach (ParkingSpace space in garage.ParkingSpaces)
        {
            string colour;

            if (space.AvailableSpace == 4)
            {
                colour = "green";
            }
            else if (space.AvailableSpace == 3)
            {
                colour = "yellow";
            }
            else if (space.AvailableSpace == 2)
            {
                colour = "darkorange";
            }
            else if (space.AvailableSpace == 1)
            {
                colour = "orangered1";
            }
            else
            {
                colour = "red";
            }

            map.Append($"[{colour}]■ {space.ID,3}[/]    ");

            count++;

            if (count % columns == 0)
            {
                map.AppendLine();
            }
        }

        return map.ToString();
    }
}