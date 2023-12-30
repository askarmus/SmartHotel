using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.Extensions;
using SmartHotel.Infrastructure.Perception;
using SmartHotel.Infrastructure.Services;

namespace SmartHotel.BookingService.Data
{
    public class BookingDbContext : BaseDbContext
    {
        public BookingDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }

        public DbSet<Entities.Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Booking>()
            .Property(u => u.PaymentStatus)
            .HasConversion<string>()
            .HasMaxLength(50);
        }

    }
}


