using MediatR;

namespace RoomService.CQRS.Commands
{
    public class DeleteRoomAvailabilityCommand : IRequest<Unit>
    {
        public int RoomAvailabilityId { get; set; }
    }
}
