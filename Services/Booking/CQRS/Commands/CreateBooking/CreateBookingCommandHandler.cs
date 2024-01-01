using SmartHotel.BookingService.Repository;
using MediatR;
using Service.Shared.Enum;
using FluentValidation;
using Data.Entities;
using SmartHotel.Abstraction.Result;
using System;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Outcome<int>>
    {
        private readonly IBookingRepository _repository;
        private readonly IValidator<CreateBookingCommand> _validator;

        public CreateBookingCommandHandler(IBookingRepository repository, IValidator<CreateBookingCommand> validator)
        {
            _repository = repository ;
            _validator = validator ;
        }

        public async Task<Outcome<int>> Handle(CreateBookingCommand createBookingCommand, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(createBookingCommand);

            var booking = new Booking
            {
                RoomId = createBookingCommand.RoomId,
                BookingDate = createBookingCommand.BookingDate,
                PaymentStatus = PaymentStatus.Pending
            };

            var result =  await _repository.CreateBookingAsync(booking);

            return Outcome<int>.Success(result);
        }
    }
}
