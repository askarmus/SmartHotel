using BookingService.CQRS.Commands.CreateRoom;
using BookingService.Repository;
using MediatR;

namespace BookingService.CQRS.Commands.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IBookingRepository _repository;

        public CreateBookingCommandHandler(IBookingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(CreateBookingCommand createBookingDto, CancellationToken cancellationToken)
        {
            var booking = new Entities.Booking
            {
                UserId = createBookingDto.UserId,
                RoomId = createBookingDto.RoomId,
                CheckInDate = createBookingDto.CheckInDate,
                CheckOutDate = createBookingDto.CheckOutDate,
                BookingStatus = BookingStatus.Pending
            };

            return await _repository.CreateBookingAsync(booking);
        }
    }
}
