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
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
