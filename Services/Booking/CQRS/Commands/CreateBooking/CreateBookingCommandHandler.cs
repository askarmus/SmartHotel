using SmartHotel.BookingService.Repository;
using MediatR;
using Service.Shared.Enum;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        private readonly IBookingRepository _repository;
        private readonly IValidator<CreateBookingCommand> _validator;

        public CreateBookingCommandHandler(IBookingRepository repository, IValidator<CreateBookingCommand> validator)
        {
            _repository = repository  ;
            _validator = validator  ;
        }

        public async Task<int> Handle(CreateBookingCommand createBookingCommand, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(createBookingCommand);

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
