using SmartHotel.BookingService.Data;
using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace SmartHotel.BookingService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Booking> GetBookingAsync(int bookingId)
        {
            return await _context.Bookings.FirstOrDefaultAsync(w=>w.Id == bookingId);
        }

        public async Task<int> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return booking.Id;
        }

        public async Task UpdateBookingStatusAsync(Booking booking)
        {
           var existingBooking = await _context.Bookings.FindAsync(booking.Id);

            if (existingBooking != null)
            {
                existingBooking.PaymentStatus = booking.PaymentStatus;

                await _context.SaveChangesAsync();
            }
        }
    }
}
