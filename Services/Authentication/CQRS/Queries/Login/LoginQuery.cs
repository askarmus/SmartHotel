using MediatR;
using SmartHotel.AuthenticationService.CQRS.Queries.Login.Responses;

namespace SmartHotel.AuthenticationService.CQRS.Queries.Login
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; }
        public string Password { get; }
    }
}
