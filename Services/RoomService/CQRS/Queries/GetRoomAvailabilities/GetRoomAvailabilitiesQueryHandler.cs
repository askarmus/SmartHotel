using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities.Response;
using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.Abstraction.Result;
using AutoMapper;

namespace SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities
{
    public class GetRoomAvailabilitiesQueryHandler : IRequestHandler<GetRoomAvailabilitiesQuery, Result<GetRoomAvailabilitiesQueryResponse>>
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;

        public GetRoomAvailabilitiesQueryHandler(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetRoomAvailabilitiesQueryResponse>> Handle(GetRoomAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var (availabilities, totalCount) = _repository.GetAvailabilities(request);

            var roomAvailabilities = availabilities.Select(response => _mapper.Map<RoomAvailabilityDto>(response)).ToList();

            return Result<GetRoomAvailabilitiesQueryResponse>.Success(new GetRoomAvailabilitiesQueryResponse(roomAvailabilities, totalCount));
        }
    }
}