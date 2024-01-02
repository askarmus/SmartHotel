using Gridify;
using MediatR;
using SmartHotel.Abstraction.Result;
using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities.Response;

namespace SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities
{
    public class GetRoomAvailabilitiesQuery : GridifyQuery, IRequest<Result<GetRoomAvailabilitiesQueryResponse>>
    {
       
    }
}

