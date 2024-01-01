using MassTransit;
using Service.Shared;
using Service.Shared.Enum;
using Persistance.Repository;

namespace SmartHotel.BookingService.Consumers
{
    public class BookingStatusUpdateConsumer : IConsumer<BookingStatusUpdateEvent>
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingStatusUpdateConsumer(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task Consume(ConsumeContext<BookingStatusUpdateEvent> context)
        {
            var bookingId = context.Message.BookingId;

            var booking = await _bookingRepository.GetBookingAsync(bookingId);

            if (booking == null) return;

            booking.PaymentStatus = context.Message.PaymentStatus;

            await _bookingRepository.UpdateBookingStatusAsync(booking);
        }
    }

}
