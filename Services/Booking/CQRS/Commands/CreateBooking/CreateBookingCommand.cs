﻿using MediatR;

namespace SmartHotel.BookingService.CQRS.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<int>
    {
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public double Amount { get; set; }
        public required string CreditCardNumber { get; set; }
    }
}

