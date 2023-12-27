namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking.Request
{
    public record CreateBookingRequest(int BookingId, int RoomId, DateTime BookingDate, BookingStatus BookingStatus, string CreditCardNumber, double Amount);
}