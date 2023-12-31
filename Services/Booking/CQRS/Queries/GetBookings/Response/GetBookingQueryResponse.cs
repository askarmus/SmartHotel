namespace SmartHotel.BookingService.CQRS.Queries.GetBookings.Response
{
    public class GetBookingsQueryResponse
    {
        public GetBookingsQueryResponse(int bookingId, int roomId, DateTime bookingDate, string paymentStatus)
        {
            BookingId = bookingId;
            RoomId = roomId;
            BookingDate = bookingDate;
            PaymentStatus = paymentStatus;
        }

        public GetBookingsQueryResponse()
        {
        }


        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public string? PaymentStatus { get; set; }
    }
}

