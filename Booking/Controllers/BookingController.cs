using AutoMapper;
using BookingService.CQRS.Commands;
using BookingService.CQRS.Queries;
using BookingService.Dto;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BookingController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("GetBooking/{bookingId}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            var result = await _mediator.Send(new GetBookingQuery { BookingId = bookingId });


            if (result != null)
            {
                return Ok(_mapper.Map<BookingDto>(result));
            }

            return NotFound();
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
        {
            var bookingId = await _mediator.Send(new CreateBookingCommand
            {
                UserId = createBookingDto.UserId,
                RoomId = createBookingDto.RoomId,
                CheckInDate = createBookingDto.CheckInDate,
                CheckOutDate = createBookingDto.CheckOutDate
            });

            await _publishEndpoint.Publish(new BookingCreatedEvent
            {
                BookingId = bookingId,
            });

            return Ok(bookingId);
        }
    }
}