using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.Persistence;

namespace SmartHotel.BookingService.Data
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
