using BookingService.CQRS.Queries.IsRoomAvailable.Response;
using MediatR;

namespace BookingService.CQRS.Queries.IsRoomAvailable
{
    public class IsRoomAvailabilityGuery : IRequest<IsRoomAvailabilityQueryResponse>
    {
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

