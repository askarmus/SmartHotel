using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoomService.CQRS.Commands;
using RoomService.DTO;
using RoomService.CQRS.Queries;

namespace AvailabilityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomAvailabilityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoomAvailabilityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetRoomAvailability/{id}")]
        public async Task<ActionResult<RoomAvailabilityDto>> GetRoomAvailability(int id)
        {
            var query = new GetRoomAvailabilitiesQuery { RoomAvailabilityId = id };
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<RoomAvailabilityDto>(result));
        }

        [HttpGet("GetRoomAvailabilitiesByDate/{date}")]
        public async Task<ActionResult<List<RoomAvailabilityDto>>> GetRoomAvailabilitiesByDate(string date)
        {
            var query = new GetRoomAvailabilitiesQuery { Date = System.DateTime.Parse(date) };
            var result = await _mediator.Send(query);
            return Ok(_mapper.Map<List<RoomAvailabilityDto>>(result));
        }

        [HttpPost("CreateRoomAvailability")]
        public async Task<ActionResult<int>> CreateRoomAvailability(CreateRoomAvailabilityCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateRoomAvailability")]
        public async Task<ActionResult> UpdateRoomAvailability(UpdateRoomAvailabilityCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("DeleteRoomAvailability/{id}")]
        public async Task<ActionResult> DeleteRoomAvailability(int id)
        {
            var command = new DeleteRoomAvailabilityCommand { RoomAvailabilityId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
