using AutoMapper;
using HvZ_backend.Data.DTOs.Kills;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class KillProfile : Profile
    {
        public KillProfile()
        {

            CreateMap<Kill, KillDTO>()
                .ForMember(kdto => kdto.PlayerId, opt => opt.MapFrom(k => k.PlayerId));
            //.ForMember(kdto => kdto.LocationId, opt => opt.MapFrom(k => k.Player.Location.Id))



            CreateMap<Kill, KillPostDTO>().ReverseMap();

            CreateMap<Kill, KillPutDTO>().ReverseMap();
        }
    }
}
