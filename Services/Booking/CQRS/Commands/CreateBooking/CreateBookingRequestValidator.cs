using FluentValidation;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking
{
    public class CreateBookingRequestValidator :  AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(x => x.BookingDate)
             .NotEmpty().WithMessage("Check-in date cannot be empty.")
             .Must(BeGreaterThanOrEqualToCurrentDate).WithMessage("Check-in date must be greater than or equal to the current date.");
        }

         private bool BeGreaterThanOrEqualToCurrentDate(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }
    }
}
