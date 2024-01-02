using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities.Response;
using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.Abstraction.Result;
using System.Collections.Generic;
using AutoMapper;

namespace SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities
{
    public class GetRoomAvailabilitiesQueryHandler : IRequestHandler<GetRoomAvailabilitiesQuery, Outcome<GetRoomAvailabilitiesQueryResponse>>
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;

        public GetRoomAvailabilitiesQueryHandler(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Outcome<GetRoomAvailabilitiesQueryResponse>> Handle(GetRoomAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var (availabilities, totalCount) = _repository.GetAvailabilities(request);

            var roomAvailabilities = availabilities.Select(response => _mapper.Map<RoomAvailabilityDto>(response)).ToList();

            return Outcome<GetRoomAvailabilitiesQueryResponse>.Success(new GetRoomAvailabilitiesQueryResponse(roomAvailabilities, totalCount));

        }
    }
}