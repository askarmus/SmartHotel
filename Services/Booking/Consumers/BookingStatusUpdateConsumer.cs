using SmartHotel.BookingService.Repository;
using MassTransit;
using Service.Shared;
using Service.Shared.Enum;

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
            var paymentStatus = context.Message.PaymentStatus;
            var availabilityStatus = context.Message.AvailabilityStatus;

            var booking = await _bookingRepository.GetBookingAsync(bookingId);

            if (booking == null)
            {
                return;
            }

            booking.BookingStatus = (paymentStatus == PaymentStatus.Success)
                ? BookingStatus.Confirmed
                : BookingStatus.Cancelled;

            await _bookingRepository.UpdateBookingStatusAsync(booking);
        }
    }

}
