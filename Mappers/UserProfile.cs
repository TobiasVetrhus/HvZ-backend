using AutoMapper;
using HvZ_backend.Data.DTOs.Users;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserPostDTO>().ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(udto => udto.Players, options => options
                    .MapFrom(u => u.Players.Select(u => u.Id).ToArray()));

            CreateMap<User, UserPutDTO>().ReverseMap();
        }
    }
}
