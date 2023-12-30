using SmartHotel.BookingService.Repository;
using MediatR;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IBookingRepository _repository;

        public CreateBookingCommandHandler(IBookingRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(CreateBookingCommand createBookingCommand, CancellationToken cancellationToken)
        {
            var booking = new Entities.Booking
            {
                RoomId = createBookingCommand.RoomId,
                BookingDate = createBookingCommand.BookingDate,
                BookingStatus = BookingStatus.Pending
            };

            return await _repository.CreateBookingAsync(booking);
        }
    }
}
