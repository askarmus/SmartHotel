using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SmartHotel.Abstraction;

namespace SmartHotel.AuthenticationService.Exceptions
{
    public class RegisterException : SmartHotelValidationException
    {
        public RegisterException(IEnumerable<IdentityError> errors) : base("Unable to register account.", 102)
        {
            ValidationMessages = errors.Select(x => x.Description).ToList();
        }
    }
}
