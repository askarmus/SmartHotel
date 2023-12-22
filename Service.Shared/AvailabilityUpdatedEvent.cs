using Service.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared
{
    public class AvailabilityUpdatedEvent
    {
        public int BookingId { get; set; }
        public AvailabilityStatus AvailabilityStatus { get; set; }
    }
}
