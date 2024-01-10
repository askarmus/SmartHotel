namespace SmartHotel.AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost("LogIn")]
    public async Task<ActionResult<LoginResponse>> LogIn(LoginRequest request)
    {
        _logger.LogInformation("LogIn from UsersController called");
        return Ok(await _mediator.Send(new LoginQuery(request.UserName, request.Password)));
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequest request)
    {
        await _mediator.Send(new RegisterUserCommand(request.UserName, request.Password, request.Name, request.Password));

        return Ok();
    }


    [HttpGet("GetUserDetails/{userId}")]
    public async Task<ActionResult<UserDto>> GetUserDetails(string userId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetUserDetailsQuery(userId), cancellationToken));
    }
}