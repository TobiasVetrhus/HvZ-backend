using AutoMapper;
using HvZ_backend.Data.DTOs.PlayerKillRoles;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class PlayerKillRoleProfile : Profile
    {
        public PlayerKillRoleProfile()
        {
            CreateMap<PlayerKillRole, PlayerKillRolePostDTO>().ReverseMap();
            CreateMap<PlayerKillRole, PlayerKillRolePutDTO>().ReverseMap();
            CreateMap<PlayerKillRole, PlayerKillRoleDTO>().ReverseMap();
        }
    }
}
