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
           var existingBooking = await _context.Bookings.FindAsync(booking.Id);

            if (existingBooking != null)
            {
                existingBooking.PaymentStatus = booking.PaymentStatus;

                await _context.SaveChangesAsync();
            }
        }
    }
}
