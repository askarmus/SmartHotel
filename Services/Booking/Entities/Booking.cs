using SmartHotel.Core;
using System.ComponentModel.DataAnnotations;

namespace SmartHotel.BookingService.Entities
{
    public class Booking : BaseEntity
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public long Version { get; set; }

    }

}
