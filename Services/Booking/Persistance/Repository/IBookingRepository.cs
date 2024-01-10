using Persistance.Entities;

namespace Persistance.Repository;

public interface IBookingRepository
{
    Task<Booking> GetBookingAsync(int bookingId);
    Task<List<Booking>> GetBookingsAsync();
    Task UpdateBookingStatusAsync(Booking booking);
    Task<int> CreateBookingAsync(Booking booking);
}
