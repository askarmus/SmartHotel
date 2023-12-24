using MediatR;

namespace BookingService.CQRS.Commands
{
    public class CreateBookingCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
