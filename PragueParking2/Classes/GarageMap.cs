using System.Text;

namespace PragueParking2.Classes;

internal static class GarageMap
{
    
    internal static string Build(ParkingGarage garage)
    
    {
        var map = new StringBuilder();
        
        int columns = 10;
        int count = 0;

        map.AppendLine("[green]Empty[/] | [yellow]Partly occupied[/] | [red]Full[/]");
        
        map.AppendLine();

        foreach (ParkingSpace space in garage.ParkingSpaces)
            
        {
            
            string colour;

            if (space.IsEmpty)
                
            {
                colour = "green";
            }
            
            else if (space.AvailableSpace > 0)
                
            {
                colour = "yellow";
            }
            
            else
            
            {
                colour = "red";
            }

            string symbol;

            if (space.IsEmpty)
                
            {
                symbol = "░░░░";
            }
            
            else if (space.AvailableSpace > 0)
                
            {
                symbol = "▓▓░░";
            }
            
            else
            
            {
                symbol = "████";
            }

            map.Append($"[{colour}]{symbol} {space.ID,3}[/]  ");            
            
            count = count + 1;

            if (count % columns == 0)
                
            {
                map.AppendLine();
            }
            
        }

        return map.ToString();
        
    }
}