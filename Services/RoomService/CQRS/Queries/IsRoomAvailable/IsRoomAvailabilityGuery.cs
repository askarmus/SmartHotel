using SmartHotel. BookingService.CQRS.Queries.IsRoomAvailable.Response;
using MediatR;
using SmartHotel.Abstraction.Result;

namespace SmartHotel.BookingService.CQRS.Queries.IsRoomAvailable
{
    public class IsRoomAvailabilityGuery : IRequest<Result<IsRoomAvailabilityQueryResponse>>
    {
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}

