using SmartHotel.BookingService.CQRS.Commands.CreateBooking;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking.Request;
using SmartHotel.BookingService.CQRS.Queries.GetBooking;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace SmartHotel.BookingService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<AvailabilityUpdatedEvent> _client;

        public BookingController(IMediator mediator, IPublishEndpoint publishEndpoint, IRequestClient<AvailabilityUpdatedEvent> client)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _publishEndpoint = publishEndpoint;
            _client = client;
        }

        [HttpGet("GetBooking/{bookingId}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var result = await _mediator.Send(new GetBookingQuery { BookingId = bookingId });

            return Ok(result);
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {

            var response = await _client.GetResponse<AvailabilityUpdateResult>(new { request.RoomId, request.BookingDate });

            if (response.Message.AvailabilityStatus !=  Service.Shared.Enum.AvailabilityStatus.AlreadyBooked)
            {
                var bookingId = await _mediator.Send(new CreateBookingCommand
                {
                    RoomId = request.RoomId,
                    BookingDate = request.BookingDate,
                    Amount = request.Amount,
                    CreditCardNumber = request.CreditCardNumber,
                });

                await _publishEndpoint.Publish(new BookingCreatedEvent
                {
                    CreditCardNumber = request.CreditCardNumber,
                    Amount = request.Amount,
                    BookingId = bookingId,
                });
                
                return Ok(bookingId);

            } 
            else
            {
                return BadRequest($"Booking is not avialbe for {request.BookingDate}");
            }
            

        }
    }
}