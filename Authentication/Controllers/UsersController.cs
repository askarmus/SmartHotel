using AuthenticationService.CQRS.Commands.Register;
using AuthenticationService.CQRS.Commands.Register.Requests;
using AuthenticationService.CQRS.Queries.GetUserDetails;
using AuthenticationService.CQRS.Queries.Login;
using AuthenticationService.CQRS.Queries.Login.Requests;
using AuthenticationService.CQRS.Queries.Login.Responses;
using AuthenticationService.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("LogIn")]
        public async Task<ActionResult<LoginResponse>> LogIn(LoginRequest request)
        {
            return Ok(await _mediator.Send(new LoginQuery(request.UserName, request.Password)));
        }

        [HttpPost]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            await _mediator.Send(new RegisterUserCommand(request.UserName, request.Password, request.Name, request.Password));

            return Ok();
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserDetails(string userId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetUserDetailsQuery(userId), cancellationToken));
        }
    }
}