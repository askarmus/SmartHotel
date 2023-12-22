using BookingService.Dto;
using MediatR;

namespace BookingService.CQRS.Queries
{
    public class GetBookingQuery : IRequest<Entities.Booking>
    {
        public int BookingId { get; set; }
    }
}
