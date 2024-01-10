namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking;

public class CreateBookingRequestValidator :  AbstractValidator<CreateBookingCommand>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.BookingDate)
         .NotEmpty().WithMessage("Booking date cannot be empty.")
         .Must(BeGreaterThanOrEqualToCurrentDate).WithMessage("Booking date must be greater than or equal to the current date.");
    }

     private bool BeGreaterThanOrEqualToCurrentDate(DateTime date)
    {
        return date.Date >= DateTime.Now.Date;
    }
}
