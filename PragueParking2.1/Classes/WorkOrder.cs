namespace PragueParking2.Classes;

public record WorkOrder(string Verb, VehicleType VehicleType, string VehicleRegistration, int ParkingSpaceId);
