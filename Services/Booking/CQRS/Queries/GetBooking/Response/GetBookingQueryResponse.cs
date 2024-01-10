
namespace SmartHotel.BookingService.CQRS.Queries.GetBooking.Response;

public class GetBookingQueryResponse
{
    public GetBookingQueryResponse(int bookingId, int roomId, DateTime bookingDate, string paymentStatus)
    {
        BookingId = bookingId;
        RoomId = roomId;
        BookingDate = bookingDate;
        PaymentStatus = paymentStatus;
    }

    public int BookingId { get; }
    public int RoomId { get; }
    public DateTime BookingDate { get; }
    public string PaymentStatus { get; }
}

