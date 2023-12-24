using System.ComponentModel.DataAnnotations;

namespace RoomService.Entityty
{
    public class RoomAvailability
    {
        [Key]
        public int RoomAvailabilityId { get; set; }
        public int RoomId { get; set; }
        public int AvailableUnits { get; set; }
        public DateTime Date { get; set; }
    }
}
