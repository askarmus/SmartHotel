using Service.Shared.Enum;
using SmartHotel.RoomService.Persistance.Repository;

namespace SmartHotel.BookingService.Consumers;
public class AvailabilityUpdatedEventConsumer : IConsumer<AvailabilityUpdatedEvent>
{
    private readonly IRoomRepository _bookingRepository;

    public AvailabilityUpdatedEventConsumer(IRoomRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task Consume(ConsumeContext<AvailabilityUpdatedEvent> context)
    {
        var roomId = context.Message.RoomId;
        var bookingDate = context.Message.BookingDate;

        var booking = await _bookingRepository.IsRoomAvailable(roomId, bookingDate);

        if (booking)
        {
            await context.RespondAsync<AvailabilityUpdateResult>(new
            {
                BookingId = context.Message.RoomId,
                AvailabilityStatus = AvailabilityStatus.AlreadyBooked
            });
        }

        await _bookingRepository.CreateBookingStatus(roomId, bookingDate);

        await context.RespondAsync<AvailabilityUpdateResult>(new
        {
            BookingId = context.Message.RoomId,
            AvailabilityStatus = AvailabilityStatus.Booked
        });
    }
}

