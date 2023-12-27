using BookingService.CQRS.Queries.IsRoomAvailable;
using BookingService.CQRS.Queries.IsRoomAvailable.Response;
using BookingService.Repository;
using MediatR;
using RoomService.Entityty;
using SmartHotel.Exceptions.Abstraction;

namespace BookingService.CQRS.Queries.GetBooking
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