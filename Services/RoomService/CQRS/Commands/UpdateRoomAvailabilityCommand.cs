using MediatR;

namespace RoomService.CQRS.Commands
{
    public class UpdateRoomAvailabilityCommand : IRequest<Unit>
    {
        public int RoomAvailabilityId { get; set; }
        public int AvailableUnits { get; set; }
    }
}
