using Microsoft.EntityFrameworkCore;
using RoomService.Data;
using RoomService.Entityty;

namespace RoomService.Repositories
{
    public class RoomAvailabilityRepository : IRoomAvailabilityRepository
    {
        private readonly RoomDbContext _context;

        public RoomAvailabilityRepository(RoomDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomAvailability>> GetAvailabilitiesAsync()
        {
            return await _context.RoomAvailabilities.ToListAsync();
        }

        public async Task<RoomAvailability> GetAvailabilityByIdAsync(int availabilityId)
        {
            return await _context.RoomAvailabilities.FindAsync(availabilityId);
        }

        public async Task<List<RoomAvailability>> GetAvailabilitiesByDateAsync(DateTime date)
        {
            return await _context.RoomAvailabilities
                .Where(ra => ra.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task AddAvailabilityAsync(RoomAvailability availability)
        {
            _context.RoomAvailabilities.Add(availability);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAvailabilityAsync(RoomAvailability availability)
        {
            _context.RoomAvailabilities.Update(availability);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAvailabilityAsync(int availabilityId)
        {
            var availability = await _context.RoomAvailabilities.FindAsync(availabilityId);
            if (availability != null)
            {
                _context.RoomAvailabilities.Remove(availability);
                await _context.SaveChangesAsync();
            }
        }
    }
}
