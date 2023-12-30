using AutoMapper;
using SmartHotel. BookingService.CQRS.Queries.GetBooking;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Shared;

namespace SmartHotel. BookingService.Controllers
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
           // var result = await _mediator.Send(new GetBookingQuery { BookingId = bookingId });

            return Ok(null);
        }
         
    }
}