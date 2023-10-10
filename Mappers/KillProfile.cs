using AutoMapper;
using HvZ_backend.Data.DTOs.Kills;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class KillProfile : Profile
    {
        public KillProfile()
        {
           
            CreateMap<Kill, KillDTO>().ReverseMap();
 
            CreateMap<Kill, KillPostDTO>().ReverseMap();

            CreateMap<Kill, KillPutDTO>().ReverseMap();
        }
    }
}
