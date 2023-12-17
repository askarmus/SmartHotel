using MediatR;
using RoomService.CQRS.Queries;
using RoomService.Entityty;
using RoomService.Repositories;

namespace RoomService.CQRS
{
    public class GetRoomAvailabilitiesQueryHandler : IRequestHandler<GetRoomAvailabilitiesQuery, RoomAvailability>
    {
        private readonly IRoomAvailabilityRepository _repository;

        public GetRoomAvailabilitiesQueryHandler(IRoomAvailabilityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<RoomAvailability> Handle(GetRoomAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var roomAvailabilities = await _repository.GetAvailabilityByIdAsync(request.RoomAvailabilityId);
            return roomAvailabilities;
        }
    }
}
