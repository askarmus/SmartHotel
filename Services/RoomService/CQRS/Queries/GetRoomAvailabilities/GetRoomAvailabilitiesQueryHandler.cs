using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities.Response;
using SmartHotel.BookingService.Repository;
using MediatR;
using SmartHotel.Abstraction.Result;
using System.Collections.Generic;
using AutoMapper;

namespace SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities
{
    public class GetRoomAvailabilitiesQueryHandler : IRequestHandler<GetRoomAvailabilitiesQuery, Outcome<(List<GetRoomAvailabilitiesQueryResponse>,int)>>
    {
        private readonly IRoomRepository _repository;
        private readonly IMapper _mapper;

        public GetRoomAvailabilitiesQueryHandler(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Outcome<(List<GetRoomAvailabilitiesQueryResponse>, int)>> Handle(GetRoomAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.GetAvailabilities(request);

            // Use AutoMapper to map the entities
            var roomAvailabilities = result.Item1.Select(response => _mapper.Map<GetRoomAvailabilitiesQueryResponse>(response)).ToList();

            return Outcome<(List<GetRoomAvailabilitiesQueryResponse>, int)>.Success((roomAvailabilities, result.Item2));

        }
    }
}