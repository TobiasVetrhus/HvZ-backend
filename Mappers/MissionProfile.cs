using AutoMapper;
using HvZ_backend.Data.DTOs.Missions;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class MissionProfile : Profile
    {
        public MissionProfile()
        {
            CreateMap<Mission, MissionPostDTO>().ReverseMap();
            CreateMap<Mission, MissionPutDTO>().ReverseMap();
            CreateMap<Mission, MissionDTO>().ReverseMap();
        }
    }
}
