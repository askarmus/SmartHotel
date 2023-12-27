using Service.Shared.Enum;

namespace Service.Shared
{
    public class AvailabilityUpdateResult
    {
        public AvailabilityStatus AvailabilityStatus { get; set; }
        public int BookingId { get; set; }
    }
}
