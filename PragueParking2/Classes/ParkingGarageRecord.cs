namespace PragueParking2.Classes;

internal record ParkingGarageRecord(int Capacity, int ParkingSpaceCapacity, int BusCapable, List<ParkingSpaceRecord> ParkingSpaceRecords);
