using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities;

namespace SmartHotel.BookingService.Controllers; 

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RoomControlle(IMediator _mediator) : ControllerBase
{

    [HttpGet("GetRoomAvailability/{filter}/{page}/{orderBy}")]
    public async Task<IActionResult> GetRoomAvailability(string filter, int page , int pageSize ,  string orderBy )
    {
        var result = await _mediator.Send(new GetRoomAvailabilitiesQuery() { Filter = filter, Page = page, PageSize = pageSize, OrderBy = orderBy });

        return Ok(result);
    }
}