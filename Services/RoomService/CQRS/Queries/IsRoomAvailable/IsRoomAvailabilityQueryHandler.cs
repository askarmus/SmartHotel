using SmartHotel. BookingService.CQRS.Queries.IsRoomAvailable;
using SmartHotel. BookingService.CQRS.Queries.IsRoomAvailable.Response;
using SmartHotel. BookingService.Repository;
using MediatR;
using SmartHotel.Exceptions.Abstraction;
using SmartHotel.RoomService.Entities;

namespace SmartHotel. BookingService.CQRS.Queries.GetBooking
{
    public class IsRoomAvailabilityQueryHandler : IRequestHandler<IsRoomAvailabilityGuery, IsRoomAvailabilityQueryResponse>
    {
        private readonly IRoomRepository _repository;

        public IsRoomAvailabilityQueryHandler(IRoomRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IsRoomAvailabilityQueryResponse> Handle(IsRoomAvailabilityGuery request, CancellationToken cancellationToken)
        {
            var isAvailable =  await _repository.IsRoomAvailable(request.RoomId, request.BookingDate);
            if (!isAvailable)
                throw new NotFoundException(request.RoomId.ToString(), nameof(RoomAvailability));

            return new IsRoomAvailabilityQueryResponse(isAvailable);
        }
    }
}