using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services
{
    public class UsersDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }
    }
}
