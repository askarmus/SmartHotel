using BookingService.Entities;

namespace BookingService.CQRS.Queries.GetBooking.Response
{
    public class GetBookingQueryResponse
    {
        public GetBookingQueryResponse(int BookingId, int UserId, int RoomId, DateTime CheckInDate, DateTime CheckOutDate, BookingStatus BookingStatus)
        {
            BookingId = BookingId;
            UserId = UserId;
            RoomId = RoomId;
            CheckInDate = CheckInDate;
            CheckOutDate = CheckOutDate;
            BookingStatus = BookingStatus;
        }
        public int BookingId { get; }
        public string UserId { get; }
        public string CheckInDate { get; }
        public string CheckOutDate { get; }
        public string Name { get; }
        public BookingStatus BookingStatus { get; }
    }
}