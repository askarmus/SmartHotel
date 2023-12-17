using FluentValidation;
using RoomService.CQRS.Commands;

namespace RoomService.Validation
{
    public class AddRoomtValidator : AbstractValidator<CreateRoomAvailabilityCommand>
    {
        public AddRoomtValidator()
        {
            RuleFor(x => x.AvailableUnits).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
