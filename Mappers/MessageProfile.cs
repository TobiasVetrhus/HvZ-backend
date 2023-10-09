using AutoMapper;
using HvZ_backend.Data.DTOs.Messages;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessagePostDTO>().ReverseMap();

            CreateMap<Message, MessageDTO>().ReverseMap();

            CreateMap<Message, MessagePutDTO>().ReverseMap();
        }
    }
}
