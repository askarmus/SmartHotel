namespace SmartHotel.AuthenticationService.CQRS.Commands.Register.Requests
{
    public class RegisterRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
