using AutoMapper;
using HvZ_backend.Data.DTOs.Users;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserPostDTO>().ReverseMap();

            CreateMap<AppUser, AppUserDTO>()
                .ForMember(udto => udto.Players, options => options
                    .MapFrom(u => u.Players.Select(u => u.Id).ToArray()));

            CreateMap<AppUser, AppUserPutDTO>().ReverseMap();
        }
    }
}
