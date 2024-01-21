using Asp.Versioning;

namespace SmartHotel.BookingService.Controllers.v2;

[ApiVersion("2.0")]
[ApiController]
[Authorize]
[Route("api/v{version:apiversion}/[controller]")]
public class BookingController(IMediator _mediator) : ControllerBase
{
    [MapToApiVersion("2.0")]
    [HttpGet("GetBooking/{bookingId}")]
    public async Task<IActionResult> GetBooking(int bookingId)
    {
        var result = await _mediator.Send(new GetBookingQuery { BookingId = bookingId });

        return Ok(result);
    }
}


