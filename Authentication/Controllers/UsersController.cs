using Authentication.Commands.Register;
using Authentication.Commands.Register.Requests;
using Authentication.Queries;
using Authentication.Queries.Login;
using Authentication.Queries.Login.Requests;
using Authentication.Queries.Login.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.User.Contracts;


namespace Authentication.Controllers
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