using SmartTicket.Core;

namespace BookingService.Entities
{
    public class Booking : BaseEntity
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }

}
