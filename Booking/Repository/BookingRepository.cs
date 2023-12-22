using BookingService.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Entities.Booking> GetBookingAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }

        public async Task<IEnumerable<Entities.Booking>> GetBookingsAsync(int userId)
        {
            return await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<int> CreateBookingAsync(Entities.Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking.BookingId;
        }

        public Task UpdateBookingStatusAsync(Entities.Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
