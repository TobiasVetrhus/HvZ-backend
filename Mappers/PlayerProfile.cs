using AutoMapper;
using HvZ_backend.Data.DTOs.Player;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            // Mapping from Player to PlayerPostDTO and vice versa
            CreateMap<Player, PlayerPostDTO>().ReverseMap();

            // Mapping from Player to PlayerDTO
            CreateMap<Player, PlayerDTO>()
                .ForMember(pdto => pdto.Messages, options => options
                    .MapFrom(p => p.Messages.Select(m => m.Id).ToArray()))
                .ForMember(pdto => pdto.Kills, options => options
                    .MapFrom(p => p.Kills.Select(k => k.Id).ToArray()));

            // Mapping from Player to PlayerPutDTO and vice versa
            CreateMap<Player, PlayerPutDTO>().ReverseMap();
        }
    }
}