﻿using Data.Entities;

namespace SmartHotel.BookingService.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingAsync(int bookingId);
        Task UpdateBookingStatusAsync(Booking booking);
        Task<int> CreateBookingAsync(Booking booking);
    }
}
