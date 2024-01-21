using Asp.Versioning;

namespace SmartHotel.BookingService.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiversion}/[controller]")]
   
    public class BookingController(IMediator _mediator, IMapper _mapper, IPublishEndpoint _publishEndpoint, IRequestClient<AvailabilityUpdatedEvent> _client) : ControllerBase
    {
      
        [MapToApiVersion("1.0")]
        [HttpGet("GetBookings")]
        public async Task<IActionResult> GetBookings()
        {
            var result = await _mediator.Send(new GetBookingsQuery { });

            return Ok(result);
        }

        [MapToApiVersion("1.0")]
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var response = await _client.GetResponse<AvailabilityUpdateResult>(new { request.RoomId, request.BookingDate });

            if (response.Message.AvailabilityStatus != Service.Shared.Enum.AvailabilityStatus.AlreadyBooked)
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
