using SmartHotel.BookingService.Repository;
using MediatR;
using Service.Shared.Enum;

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
                PaymentStatus = PaymentStatus.Pending
            };

            return await _repository.CreateBookingAsync(booking);
        }
    }
}
