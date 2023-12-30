using Data.Entities;
using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.Perception;
using SmartHotel.Infrastructure.Services;

namespace SmartHotel.BookingService.Data
{
    public class BookingDbContext : BaseDbContext
    {
        public BookingDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
            .Property(u => u.PaymentStatus)
            .HasConversion<string>()
            .HasMaxLength(50);
        }

    }
}


