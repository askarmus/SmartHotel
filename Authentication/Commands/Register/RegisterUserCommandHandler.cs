using MediatR;
using Microsoft.AspNetCore.Identity;
using Authentication.Entities;
using Authentication.Exceptions;

namespace Authentication.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        async Task IRequestHandler<RegisterUserCommand>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var identity = await _userManager.CreateAsync(new AppUser(request.UserName, request.Name, request.Surname),
              request.Password);
            if (!identity.Succeeded)
                throw new RegisterException(identity.Errors);
        }
    }
}