using Service.Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace RoomService.Entityty
{
    public class RoomAvailability
    {
        [Key]
        public int RoomAvailabilityId { get; set; }
        public int RoomId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime BookingDate { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }
}
