using MediatR;
using RoomService.CQRS.Commands;
using RoomService.Entityty;
using RoomService.Repositories;

namespace RoomService.CQRS.Handler
{
    public class CreateRoomAvailabilityCommandHandler : IRequestHandler<CreateRoomAvailabilityCommand, int>
    {
        private readonly IRoomAvailabilityRepository _repository;

        public CreateRoomAvailabilityCommandHandler(IRoomAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateRoomAvailabilityCommand request, CancellationToken cancellationToken)
        {

            var roomAvailability = new RoomAvailability
            {
                RoomId = request.RoomId,
                AvailableUnits = request.AvailableUnits,
                Date = request.Date,
            };

            await _repository.AddAvailabilityAsync(roomAvailability);

            return roomAvailability.RoomAvailabilityId;
        }
    }
}
