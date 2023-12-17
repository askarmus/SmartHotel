using Authentication.Exceptions;
using MediatR;
using RoomService.CQRS.Commands;
using RoomService.Repositories;

namespace RoomService.CQRS.Handler
{
    public class UpdateRoomAvailabilityHandler : IRequestHandler<UpdateRoomAvailabilityCommand, Unit>
    {
        private readonly IRoomAvailabilityRepository _repository;

        public UpdateRoomAvailabilityHandler(IRoomAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateRoomAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var roomAvailability = await _repository.GetAvailabilityByIdAsync(request.RoomAvailabilityId);

            if (roomAvailability == null)
            {
                throw new RoomAvailabilityException(request.RoomAvailabilityId);
            }

            roomAvailability.AvailableUnits = request.AvailableUnits;

            await _repository.UpdateAvailabilityAsync(roomAvailability);

            return Unit.Value;
        }
    }
}
