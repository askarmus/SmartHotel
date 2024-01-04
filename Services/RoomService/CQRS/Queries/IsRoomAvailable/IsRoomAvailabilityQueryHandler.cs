using SmartHotel.BookingService.CQRS.Queries.IsRoomAvailable;
using SmartHotel.BookingService.CQRS.Queries.IsRoomAvailable.Response;
using MediatR;
using SmartHotel.Abstraction;
using SmartHotel.Abstraction.Result;
using SmartHotel.RoomService.Persistance.Repository;
using SmartHotel.RoomService.Persistance.Entities;

namespace SmartHotel.BookingService.CQRS.Queries.GetBooking
{
    public class GetRoomAvailabilityQueryHandler : IRequestHandler<IsRoomAvailabilityGuery, Result<IsRoomAvailabilityQueryResponse>>
    {
        private readonly IRoomRepository _repository;

        public GetRoomAvailabilityQueryHandler(IRoomRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Result<IsRoomAvailabilityQueryResponse>> Handle(IsRoomAvailabilityGuery request, CancellationToken cancellationToken)
        {
            var isAvailable =  await _repository.IsRoomAvailable(request.RoomId, request.BookingDate);
            if (!isAvailable)
                throw new NotFoundException(request.RoomId.ToString(), nameof(RoomAvailability));

            return  Result<IsRoomAvailabilityQueryResponse>.Success( new IsRoomAvailabilityQueryResponse(isAvailable));
        }
    }
}