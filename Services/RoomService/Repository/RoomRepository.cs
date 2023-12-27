using BookingService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDbContext _context;

        public RoomRepository(RoomDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime bookingDatee)
        {
            var isAvailable = await _context.RoomAvailability
                .Where(a => a.RoomId == roomId && a.BookingDate == bookingDatee)
                .AllAsync(a => a.IsAvailable);

            return isAvailable;
        }
        public async Task<int> CreateBookingStatus(int roomId, DateTime bookingDatee)
        {

            var roomAvailability = new RoomService.Entityty.RoomAvailability()
            {
                BookingDate = bookingDatee,
                RoomId = roomId,
                IsAvailable = false,
                AvailabilityStatus = Service.Shared.Enum.AvailabilityStatus.Booked
            };

            await _context.RoomAvailability.AddAsync(roomAvailability);

            await _context.SaveChangesAsync();

            return roomAvailability.RoomAvailabilityId;
        }
    }
}
