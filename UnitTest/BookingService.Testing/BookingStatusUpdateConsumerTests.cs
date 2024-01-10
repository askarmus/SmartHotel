using SmartHotel.BookingService.Consumers;

namespace BookingService.Testing;

public class BookingStatusUpdateConsumerTests
{
    [Fact]
    public async Task Consume_NullBooking_ShouldNotUpdate()
    {
        // Arrange
        var bookingId = 123;

        var mockRepository = new Mock<IBookingRepository>();
        var consumer = new BookingStatusUpdateConsumer(mockRepository.Object);

        var context = Mock.Of<ConsumeContext<BookingStatusUpdateEvent>>(
            x => x.Message == new BookingStatusUpdateEvent { BookingId = bookingId, PaymentStatus = PaymentStatus.Success }
        );

        mockRepository.Setup(repo => repo.GetBookingAsync(bookingId)).ReturnsAsync((Booking)null);

        // Act
        await consumer.Consume(context);

        // Assert
        mockRepository.Verify(repo => repo.UpdateBookingStatusAsync(It.IsAny<Booking>()), Times.Never);
    }

}
