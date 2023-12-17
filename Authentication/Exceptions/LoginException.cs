using Authentication.Exceptions;
using SmartTicket.Exceptions.Abstraction;

namespace Authentication.User.Application.Exceptions
{
    public class LoginException : AppException
    {
        public LoginException() : base("Unable to login. Wrong Username or Password", 102)
        {
        }
    }
}
