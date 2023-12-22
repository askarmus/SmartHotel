using MediatR;

namespace BookingService.CQRS.Commands
{
    public class CancelBookingCommand : IRequest<bool>
    {
        public int BookingId { get; set; }
    }
}
