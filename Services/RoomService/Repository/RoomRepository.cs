using SmartHotel. BookingService.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartHotel. BookingService.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomDbContext _context;

        public RoomRepository(RoomDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime bookingDate)
        {
            var isAvailable = await _context.RoomAvailability.AnyAsync(a => a.RoomId == roomId && a.BookingDate.Date == bookingDate.Date);

            return isAvailable;
        }
        public async Task<int> CreateBookingStatus(int roomId, DateTime bookingDatee)
        {

            var roomAvailability = new RoomService.Entities.RoomAvailability()
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
