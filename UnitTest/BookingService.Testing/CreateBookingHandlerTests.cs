using FluentAssertions;
using Moq;
using Persistance.Entities;
using Persistance.Repository;
using Service.Shared.Enum;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking;

namespace BookingService.Testing; 

public class CreateBookingHandlerTests
{
    [Fact]
    public async Task Handle_ValidCreateBookingCommand_ReturnsSuccessResult()
    {
        // Arrange
        var mockRepository = new Mock<IBookingRepository>();

        var handler = new CreateBookingCommandHandler(mockRepository.Object);

        var createBookingCommand = new CreateBookingCommand
        {
            RoomId = 1,
            BookingDate = DateTime.UtcNow,
            Amount = 100.0,
            CreditCardNumber = "1234567890123456"
        };
         
        

        // Act
        var result = await handler.Handle(createBookingCommand, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();

        // Verify that CreateBookingAsync was called with the correct Booking object
        mockRepository.Verify(x => x.CreateBookingAsync(It.Is<Booking>(
            b => b.RoomId == createBookingCommand.RoomId &&
                 b.BookingDate == createBookingCommand.BookingDate &&
                 b.PaymentStatus == PaymentStatus.Pending)), Times.Once);
    }


}