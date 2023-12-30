
namespace SmartHotel.BookingService.CQRS.Queries.GetBooking.Response
{
    public class GetBookingQueryResponse
    {
        public GetBookingQueryResponse(int bookingId, int roomId, DateTime bookingDate, BookingStatus bookingStatus)
        {
            BookingId = bookingId;
            RoomId = roomId;
            BookingDate = bookingDate;
            BookingStatus = bookingStatus;
        }

        public int BookingId { get; }
        public int RoomId { get; }
        public DateTime BookingDate { get; }
        public BookingStatus BookingStatus { get; }
    }
}

