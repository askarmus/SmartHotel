using Microsoft.EntityFrameworkCore;
using RoomService.Entityty;
using SmartHotel.Infrastructure.Persistence;

namespace BookingService.Data
{
    public class RoomDbContext : BaseDbContext
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
