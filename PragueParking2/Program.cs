using FileHandlerLibrary;
using PragueParking2.Classes;
using SimpleTestLibrary;


// If available, load Configuration File into a [Config] object
// var configFilePath = "../../../config.json";
// var jsonString = FileHandler.Load(configFilePath);
// var config = Config.FromJson(jsonString);

// System.Console.WriteLine($"Garage Capacity: {config.ParkingGarageCapacity}");
// System.Console.WriteLine($"Parking Space Capacity: {config.ParkingSpaceCapacity}");
// System.Console.WriteLine($"Free Minutes: {config.FreeMinutes}");
// System.Console.WriteLine("Vehicle Size Chart:");
// foreach (var vehicleSize in config.VehicleSizes)
// {
//     System.Console.WriteLine($"- {vehicleSize.Key}: {vehicleSize.Value}");
// }
// System.Console.WriteLine("Current Rates:");
// foreach (var rate in config.Rates)
// {
//     System.Console.WriteLine($"- {rate.Key}: {rate.Value:F2} / hour");
// }


internal class Program
{
    private static void Main(string[] args)
    {
        SimpleTest.AddNewTest(
    new Test("This is a tiny test!", () => { return 2 + 2 == 4; }
));

        SimpleTest.AddNewTest(
            new Test("This is a slightly bigger test!", () =>
            {
                var foo = true;
                var bar = false;

                return foo || bar;
            }
        ));


        SimpleTest.AddNewTest(
            new Test("A Parking Garage has a Capacity", () =>
            {
                var expectedCapacity = 100;
                var parkingGarage = new ParkingGarage(expectedCapacity);

                return parkingGarage.Capacity == expectedCapacity;
            }
        ));


        // SimpleTest.AddNewTest(
        //     new Test("A Parking Space is empty by default", () =>
        //     {
        //         var parkingSpace = new ParkingSpace();

        //         return parkingSpace.IsEmpty == true;
        //     }
        // ));

        // SimpleTest.AddNewTest(
        //     new Test("An empty Parking Space does not contain any vehicles", () =>
        //     {
        //         var parkingSpace = new ParkingSpace();

        //         return parkingSpace.ParkedVehicles.Count == 0;
        //     }
        // ));



        SimpleTest.RunAllTests();

        SimpleTestResultReport.WriteReport(SimpleTest.TestResults);
    }
}