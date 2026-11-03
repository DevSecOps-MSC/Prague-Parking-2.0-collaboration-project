namespace PragueParking2.Classes;

internal record ValetTicketRecord(string VehicleRegistration, int ParkingSpaceId, decimal Rate, DateTime TimeOfCheckIn);