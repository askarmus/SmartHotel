using SmartTicket.Exceptions.Abstraction;

namespace AuthenticationService.Exceptions
{
    public class LoginException : AppException
    {
        public LoginException() : base("Unable to login. Wrong Username or Password", 102)
        {
        }
    }
}
