using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared
{
    public class BookingCreatedEvent
    {
        public int BookingId { get; set; }
        public string CreditCardNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
