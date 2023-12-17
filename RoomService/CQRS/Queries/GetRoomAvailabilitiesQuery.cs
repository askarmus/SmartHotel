using MediatR;
using RoomService.Entityty;

namespace RoomService.CQRS.Queries
{
    public class GetRoomAvailabilitiesQuery : IRequest<RoomAvailability>
    {
        public DateTime Date { get; set; }
        public int RoomAvailabilityId { get; set; }
        
    }
}
