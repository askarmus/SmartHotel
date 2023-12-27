using SmartHotel.BookingService.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartHotel.BookingService.Repository
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

        public async Task UpdateBookingStatusAsync(Entities.Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            try
            {
                var existingBooking = await _context.Bookings.FindAsync(booking.BookingId);

                if (existingBooking != null)
                {
                    existingBooking.BookingStatus = booking.BookingStatus;

                    // Update other properties as needed

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Booking with ID {booking.BookingId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update booking status.", ex);
            }
        }
    }
}
