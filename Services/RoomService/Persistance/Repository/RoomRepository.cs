using Microsoft.EntityFrameworkCore;
using Gridify;
using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities;
using SmartHotel.RoomService.Persistance.Entities;

namespace SmartHotel.RoomService.Persistance.Repository
{
    public class RoomRepository(RoomDbContext _context) : IRoomRepository
    {
        public async Task<bool> IsRoomAvailable(int roomId, DateTime bookingDate)
        {
            var isAvailable = await _context.RoomAvailability.AnyAsync(a => a.RoomId == roomId && a.BookingDate.Date == bookingDate.Date);

            return isAvailable;
        }

        public (IEnumerable<RoomAvailability>, int) GetAvailabilities(GetRoomAvailabilitiesQuery query)
        {
            var result = _context.RoomAvailability.Gridify(query);

            return (result.Data, result.Count);
        }

        public async Task<int> CreateBookingStatus(int roomId, DateTime bookingDatee)
        {
            var roomAvailability = new RoomAvailability()
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