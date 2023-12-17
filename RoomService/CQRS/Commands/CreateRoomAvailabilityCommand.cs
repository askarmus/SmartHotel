using MediatR;

namespace RoomService.CQRS.Commands
{
    public class CreateRoomAvailabilityCommand : IRequest<int>
    {
        public int RoomId { get; set; }
        public int AvailableUnits { get; set; }
        public DateTime Date { get; set; }
    }
}
