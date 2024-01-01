using AutoMapper;
using SmartHotel.BookingService.CQRS.Queries.GetRoomAvailabilities.Response;
using SmartHotel.RoomService.Data.Entities;

namespace SmartHotel.RoomService.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomAvailability, GetRoomAvailabilitiesQueryResponse>();
        }
    }
}
