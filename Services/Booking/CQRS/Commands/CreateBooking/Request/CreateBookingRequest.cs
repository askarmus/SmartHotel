namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking.Request;
public record CreateBookingRequest(int RoomId, DateTime BookingDate, string CreditCardNumber, double Amount);