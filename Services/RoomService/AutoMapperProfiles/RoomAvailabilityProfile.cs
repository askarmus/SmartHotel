using AutoMapper;
using RoomService.DTO;
using RoomService.Entityty;

namespace RoomService.AutoMapperProfiles
{
    public class RoomAvailabilityProfile : Profile
    {
        public RoomAvailabilityProfile()
        {
            CreateMap<RoomAvailability, RoomAvailabilityDto>().ReverseMap();
        }
    }
}
