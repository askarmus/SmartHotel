using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SmartTicket.Exceptions.Abstraction;

namespace Authentication.Exceptions
{
    public class RegisterException : SmartTicketValidationException
    {
        public RegisterException(IEnumerable<IdentityError> errors) : base("Unable to register account.", 102)
        {
            ValidationMessages = errors.Select(x => x.Description).ToList();
        }
    }
}
