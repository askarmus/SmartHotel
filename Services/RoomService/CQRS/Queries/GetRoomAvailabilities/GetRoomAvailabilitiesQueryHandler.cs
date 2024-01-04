using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities.Response;
using MediatR;
using SmartHotel.Abstraction.Result;
using AutoMapper;
using SmartHotel.RoomService.Persistance.Repository;

namespace SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities;

public class GetRoomAvailabilitiesQueryHandler(IRoomRepository _repository, IMapper _mapper) : IRequestHandler<GetRoomAvailabilitiesQuery, Result<GetRoomAvailabilitiesQueryResponse>>
{
    public async Task<Result<GetRoomAvailabilitiesQueryResponse>> Handle(GetRoomAvailabilitiesQuery request, CancellationToken cancellationToken)
    {
        var (availabilities, totalCount) = _repository.GetAvailabilities(request);

        var roomAvailabilities = availabilities.Select(response => _mapper.Map<RoomAvailabilityDto>(response)).ToList();

        return Result<GetRoomAvailabilitiesQueryResponse>.Success(new GetRoomAvailabilitiesQueryResponse(roomAvailabilities, totalCount));
    }
}