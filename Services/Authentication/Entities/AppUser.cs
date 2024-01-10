using Microsoft.AspNetCore.Identity;

namespace SmartHotel.AuthenticationService.Entities;

public class AppUser : IdentityUser
{
    public AppUser(string userName, string name, string surname) : base(userName)
    {
        Name = name;
        Surname = surname;
    }
    public string Name { get; set; }
    public string Surname { get; set; }
}
