using AutoMapper;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking.Request;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking;
using Service.Shared;

namespace SmartHotel.BookingService.AutoMapper
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<CreateBookingRequest, CreateBookingCommand>();
            CreateMap<CreateBookingRequest, BookingCreatedEvent>();
        }
    }
}
