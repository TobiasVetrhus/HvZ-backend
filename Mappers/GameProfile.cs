using AutoMapper;
using HvZ_backend.Data.DTOs.Games;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GamePostDTO>().ReverseMap();
            CreateMap<Game, GamePutDTO>().ReverseMap();
            CreateMap<Game, GameDTO>()
                .ForMember(gdto => gdto.RuleIds, opt => opt
                    .MapFrom(g => g.Rules
                    .Select(g => g.Id)
                    .ToArray()))
                .ForMember(gdto => gdto.PlayerIds, opt => opt
                    .MapFrom(g => g.Players
                    .Select(g => g.Id)
                    .ToArray()))
                .ForMember(gdto => gdto.MissionIds, opt => opt
                    .MapFrom(g => g.Missions
                    .Select(g => g.Id)
                    .ToArray()))
                .ForMember(gdto => gdto.ConversationIds, opt => opt
                    .MapFrom(g => g.Conversations
                    .Select(g => g.Id)
                    .ToArray()));
        }
    }
}
