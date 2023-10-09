using AutoMapper;
using HvZ_backend.Data.DTOs.Rules;
using HvZ_backend.Data.Entities;

namespace HvZ_backend.Mappers
{
    public class RuleProfile : Profile
    {
        public RuleProfile()
        {
            CreateMap<Rule, RulePostDTO>().ReverseMap();

            CreateMap<Rule, RuleDTO>()
                .ForMember(rdto => rdto.GameIds, options => options
                    .MapFrom(r => r.Games.Select(g => g.Id).ToArray()));

            CreateMap<Rule, RulePutDTO>().ReverseMap();
        }
    }
}
