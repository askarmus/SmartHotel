﻿using SmartHotel.BookingService.CQRS.Queries.GetBooking.Response;
using MediatR;

namespace SmartHotel.BookingService.CQRS.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<GetBookingQueryResponse>
    {
        public int BookingId { get; set; }
    }
}