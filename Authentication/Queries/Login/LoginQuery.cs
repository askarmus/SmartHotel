using MediatR;
using Authentication.Queries.Login.Responses;

namespace Authentication.Queries.Login
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
