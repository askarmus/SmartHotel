using Microsoft.EntityFrameworkCore;
using SmartHotel.RoomService.Data.Entities;

namespace SmartHotel.BookingService.Data
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
