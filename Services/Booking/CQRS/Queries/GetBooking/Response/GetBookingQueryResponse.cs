using SmartHotel.BookingService.Entities;

namespace SmartHotel.BookingService.CQRS.Queries.GetBooking.Response
{
    public class GetBookingQueryResponse
    {
        public GetBookingQueryResponse(int BookingId, int UserId, int RoomId, DateTime BookingDate , BookingStatus BookingStatus)
        {
            BookingId = BookingId;
            UserId = UserId;
            RoomId = RoomId;
            BookingDate = BookingDate;
            BookingStatus = BookingStatus;
        }
        public int BookingId { get; }
        public string UserId { get; }
        public string BookingDate { get; }
        public string Name { get; }
        public BookingStatus BookingStatus { get; }
    }
}