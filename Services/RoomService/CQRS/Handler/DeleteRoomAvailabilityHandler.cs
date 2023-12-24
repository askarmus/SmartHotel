using MediatR;
using RoomService.CQRS.Commands;
using RoomService.Repositories;

namespace RoomService.CQRS.Handler
{
    public class DeleteRoomAvailabilityHandler : IRequestHandler<DeleteRoomAvailabilityCommand, Unit>
    {
        private readonly IRoomAvailabilityRepository _repository;

        public DeleteRoomAvailabilityHandler(IRoomAvailabilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteRoomAvailabilityCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAvailabilityAsync(request.RoomAvailabilityId);
            return Unit.Value;
        }
    }
}
