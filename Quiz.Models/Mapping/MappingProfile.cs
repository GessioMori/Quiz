using AutoMapper;
using Quiz.Models.DTO;
using Quiz.Models.Entities;

namespace Quiz.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionDTO, Question>();
            CreateMap<QuestionaryDTO, Questionary>();
        }
    }
}
