using Microsoft.EntityFrameworkCore;
using SmartTicket.Infrastructure.Persistence;

namespace BookingService.Data
{
    public class BookingDbContext : BaseDbContext
    {
        public DbSet<Entities.Booking> Bookings { get; set; }

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, constraints, etc.
        }
    }
}
