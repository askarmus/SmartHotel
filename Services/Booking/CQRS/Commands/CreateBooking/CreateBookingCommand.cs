using MediatR;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public double Amount { get; set; }
        public string CreditCardNumber { get; set; }
    }
}

