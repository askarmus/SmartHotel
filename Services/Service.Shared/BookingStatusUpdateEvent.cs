using Service.Shared.Enum;

namespace Service.Shared
{
    public class BookingStatusUpdateEvent
    {
        public int BookingId { get; set; }
        public  PaymentStatus PaymentStatus { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }

}
