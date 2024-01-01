using AutoMapper;
using SmartHotel. BookingService.CQRS.Queries.GetBooking;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;
using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities;

namespace SmartHotel. BookingService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GeRoomAvailability/{filter}/{page}/{orderBy}")]
        public async Task<IActionResult> GeRoomAvailability(string filter, int page , string orderBy )
        {
            var result = await _mediator.Send(new GetRoomAvailabilitiesQuery() { Filter = filter, Page = page, PageSize = 10 , OrderBy = orderBy });

            return Ok(result);
        }
         
    }
}