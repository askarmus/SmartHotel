using AutoMapper;
using BookingService.Dto;

namespace BookingService.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Booking, BookingDto>();
        }
    }
}
