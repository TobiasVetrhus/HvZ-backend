using AutoMapper;
using HvZ_backend.Data.DTOs.Locations;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, LocationPostDTO>().ReverseMap();
            CreateMap<Location, LocationPutDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
        }
    }
}
