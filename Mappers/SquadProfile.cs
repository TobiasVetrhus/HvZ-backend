using AutoMapper;
using HvZ_backend.Data.DTOs.Squads;
using HvZ_backend.Data.Entities;
using System.Linq;

namespace HvZ_backend.Mappers
{
    public class SquadProfile : Profile
    {
        public SquadProfile()
        {
            CreateMap<Squad, SquadPostDTO>().ReverseMap();
            CreateMap<Squad, SquadPutDTO>().ReverseMap();
            CreateMap<Squad, SquadDTO>()
                .ForMember(gdto => gdto.PlayerIds, opt => opt
                    .MapFrom(g => g.Players
                    .Select(g => g.Id)
                    .ToArray()));

        }
    }
}
