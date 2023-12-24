namespace RoomService.DTO
{
    public class RoomAvailabilityDto
    {
        public int RoomAvailabilityId { get; set; }
        public int RoomId { get; set; }
        public int AvailableUnits { get; set; }
        public DateTime Date { get; set; }
    }
}
