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
   // [Authorize]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetRoomAvailability/{filter}/{page}/{orderBy}")]
        public async Task<IActionResult> GetRoomAvailability(string filter, int page , int pageSize ,  string orderBy )
        {
            var result = await _mediator.Send(new GetRoomAvailabilitiesQuery() { Filter = filter, Page = page, PageSize = pageSize, OrderBy = orderBy });

            return Ok(result);
        }
         
    }
}