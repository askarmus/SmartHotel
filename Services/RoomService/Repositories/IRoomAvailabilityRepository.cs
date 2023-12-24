using RoomService.Entityty;

namespace RoomService.Repositories
{
    public interface IRoomAvailabilityRepository
    {
        Task<List<RoomAvailability>> GetAvailabilitiesAsync();
        Task<RoomAvailability> GetAvailabilityByIdAsync(int availabilityId);
        Task<List<RoomAvailability>> GetAvailabilitiesByDateAsync(DateTime date);
        Task AddAvailabilityAsync(RoomAvailability availability);
        Task UpdateAvailabilityAsync(RoomAvailability availability);
        Task DeleteAvailabilityAsync(int availabilityId);
    }
}
