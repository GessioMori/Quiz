using AutoMapper;
using Quiz.Models.DTO;
using Quiz.Models.Entities;

namespace Quiz.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionDTO, Question>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ReverseMap();
            CreateMap<QuestionaryDTO, Questionary>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ReverseMap();
        }
    }
}
