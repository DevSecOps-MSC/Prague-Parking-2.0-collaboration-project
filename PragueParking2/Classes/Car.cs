using PragueParking2.Interfaces;

namespace PragueParking2.Classes;

public class Car : IVehicle
{
    public string Registration { get; set; } = "";

    public int Size => throw new NotImplementedException();
}
