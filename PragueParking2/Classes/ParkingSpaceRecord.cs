namespace PragueParking2.Classes;

internal record ParkingSpaceRecord(int Id, int Capacity, List<VehicleRecord> ParkedVehicles);