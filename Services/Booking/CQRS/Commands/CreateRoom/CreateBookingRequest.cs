namespace BookingService.CQRS.Commands.CreateRoom
{
    public record CreateBookingRequest(int BookingId, int RoomId, DateTime CheckInDate, DateTime CheckOutDate, BookingStatus BookingStatus , string CreditCardNumber, double Amount);
}