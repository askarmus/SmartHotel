using SmartHotel.BookingService.CQRS.Commands.CreateBooking;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking.Request;
using SmartHotel.BookingService.CQRS.Queries.GetBooking;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using SmartHotel.BookingService.CQRS.Queries.GetBookings;
using AutoMapper;
using SmartHotel.Abstraction.Result;
using static MassTransit.ValidationResultExtensions;

namespace SmartHotel.BookingService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BookingController(IMediator _mediator, IMapper _mapper, IPublishEndpoint _publishEndpoint, IRequestClient<AvailabilityUpdatedEvent> _client) : ControllerBase
    {

        [HttpGet("GetBooking/{bookingId}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var result = await _mediator.Send(new GetBookingQuery { BookingId = bookingId });

            return Ok(result);
        }

        [HttpGet("GetBookings")]
        public async Task<IActionResult> GetBookings(int bookingId)
        {
            var result = await _mediator.Send(new GetBookingsQuery { });

            return Ok(result);
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var response = await _client.GetResponse<AvailabilityUpdateResult>(new { request.RoomId, request.BookingDate });

            if (response.Message.AvailabilityStatus !=  Service.Shared.Enum.AvailabilityStatus.AlreadyBooked)
            {
                var bookingCommand = _mapper.Map<CreateBookingCommand>(request);
                var result = await _mediator.Send(bookingCommand);
                var bookingCreatedEvent = _mapper.Map<BookingCreatedEvent>(request);

                await _publishEndpoint.Publish(bookingCreatedEvent);
                
                return Ok(result);
            } 
            else
            {
                return BadRequest(Result<Error>.Failure(BookingErrors.Unavailable(request.BookingDate)));
            }
        }
    }
}