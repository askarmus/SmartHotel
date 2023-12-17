namespace AuthenticationService.DTO
{
    public class UserDto
    {
        public UserDto(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }

        public string FullName => $"{Name} {Surname}";
    }
}