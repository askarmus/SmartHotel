using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared
{
    public class BookingCreatedEvent : INotification
    {
        public int BookingId { get; set; }
        public required string CreditCardNumber { get; set; }
        public double Amount { get; set; }
    }
}
