using AutoMapper;
using HvZ_backend.Data.DTOs.Player;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            // Mapping from Player entity to PlayerDTO and the reverse
            CreateMap<Player, PlayerDTO>().ReverseMap();

            // Mapping from PlayerPostDTO to Player entity and the reverse
            CreateMap<PlayerPostDTO, Player>().ReverseMap();

            // Mapping from PlayerPutDTO to Player entity and the reverse
            CreateMap<PlayerPutDTO, Player>().ReverseMap();
        }
    }
}
