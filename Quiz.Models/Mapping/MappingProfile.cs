using AutoMapper;
using Quiz.Models.DTO;
using Quiz.Models.Entities;

namespace Quiz.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<QuestionaryDTO, Questionary>().ReverseMap();
            CreateMap<QuestionAnswerDTO, QuestionAnswer>().ReverseMap();
            CreateMap<QuestionaryAnswerDTO, QuestionaryAnswer>().ReverseMap();
        }
    }
}
