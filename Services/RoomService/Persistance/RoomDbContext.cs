using Microsoft.EntityFrameworkCore;
using SmartHotel.RoomService.Persistance.Entities;

namespace SmartHotel.RoomService.Persistance
{
    public class RoomDbContext : DbContext
    {
        public DbSet<RoomAvailability> RoomAvailability { get; set; }

        public RoomDbContext(DbContextOptions<RoomDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
        }
    }
}
