
namespace BookingService.Dto
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}
