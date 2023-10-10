using AutoMapper;
using HvZ_backend.Data.DTOs.Conversations;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            CreateMap<Conversation, ConversationPostDTO>().ReverseMap();

            CreateMap<Conversation, ConversationDTO>()
                .ForMember(cdto => cdto.Messages, options => options
                    .MapFrom(c => c.Messages.Select(u => u.Id).ToArray()));

            CreateMap<Conversation, ConversationPutDTO>().ReverseMap();
        }
    }
}
