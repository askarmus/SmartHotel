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
            return await _context.Bookings.FirstOrDefaultAsync(w=>w.Id == bookingId);
        }


        public async Task<int> CreateBookingAsync(Entities.Booking booking)
        {
            _context.Bookings.Add(booking);

            await _context.SaveChangesAsync();

            return booking.Id;
        }

        public async Task UpdateBookingStatusAsync(Entities.Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            try
            {
                var existingBooking = await _context.Bookings.FindAsync(booking.Id);

                if (existingBooking != null)
                {
                    existingBooking.BookingStatus = booking.BookingStatus;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Booking with ID {booking.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update booking status.", ex);
            }
        }
    }
}
