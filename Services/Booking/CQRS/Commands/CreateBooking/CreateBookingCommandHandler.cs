using Service.Shared.Enum;
using Persistance.Repository;
using Persistance.Entities;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking;

public class CreateBookingCommandHandler(IBookingRepository repository  ) : IRequestHandler<CreateBookingCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateBookingCommand createBookingCommand, CancellationToken cancellationToken)
    {
       // validator.ValidateAndThrow(createBookingCommand);

        var booking = new Booking
        {
            RoomId = createBookingCommand.RoomId,
            BookingDate = createBookingCommand.BookingDate,
            PaymentStatus = PaymentStatus.Pending
        };

        var result =  await repository.CreateBookingAsync(booking);

        return Result<int>.Success(result);
    }
}
