using AutoMapper;
using BookingService.CQRS.Commands.CreateRoom;
using BookingService.CQRS.Queries.GetBooking;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace BookingService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public BookingController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _publishEndpoint = publishEndpoint;
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
            var bookingId = await _mediator.Send(new CreateBookingCommand
            {
                UserId = 10,
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate
            });

            await _publishEndpoint.Publish(new BookingCreatedEvent
            {
                CreditCardNumber = request.CreditCardNumber,
                Amount = request.Amount,
                BookingId = bookingId,
            });

            return Ok(bookingId);
        }
    }
}