using AutoMapper;
using Quiz.Models.DTO;
using Quiz.Models.Entities;
using Quiz.Models.Mapping;
using Xunit;

namespace Quiz.Models.Tests.Mapping
{
    public class QuestionaryMappingTests
    {
        private readonly Mapper _mapper;

        public QuestionaryMappingTests()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this._mapper = new Mapper(config);
        }

        [Fact]
        public void Should_Map_QuestionDTO_To_Question()
        {
            QuestionDTO questionDTO = new()
            {
                Text = "Sample Question",
                Alternatives = ["Alt1", "Alt2", "Alt3"],
                CorrectAlternative = 1
            };

            Question question = _mapper.Map<Question>(questionDTO);

            Assert.Equal(questionDTO.Text, question.Text);
            Assert.Equal(questionDTO.Alternatives, question.Alternatives);
            Assert.Equal(questionDTO.CorrectAlternative, question.CorrectAlternative);
        }

        [Fact]
        public void Should_Map_QuestionaryDTO_To_Questionary()
        {
            QuestionaryDTO questionaryDTO = new()
            {
                Title = "Sample Questionary",
                IsAvailable = true,
                Questions =
                [
                    new QuestionDTO
                    {
                        Text = "Sample Question",
                        Alternatives = ["Alt1", "Alt2"],
                        CorrectAlternative = 0
                    }
                ]
            };

            Questionary questionary = _mapper.Map<Questionary>(questionaryDTO);

            Assert.Equal(questionaryDTO.Title, questionary.Title);
            Assert.Equal(questionaryDTO.IsAvailable, questionary.IsAvailable);
            Assert.Equal(questionaryDTO.Questions.Count, questionary.Questions.Count);

            QuestionDTO firstQuestionDTO = questionaryDTO.Questions[0];
            Question firstQuestion = questionary.Questions[0];

            Assert.Equal(firstQuestionDTO.Text, firstQuestion.Text);
            Assert.Equal(firstQuestionDTO.Alternatives, firstQuestion.Alternatives);
            Assert.Equal(firstQuestionDTO.CorrectAlternative, firstQuestion.CorrectAlternative);
        }

        [Fact]
        public void Should_Map_Empty_QuestionaryDTO_To_Questionary()
        {
            QuestionaryDTO questionaryDTO = new()
            {
                Title = string.Empty,
                IsAvailable = false,
                Questions = []
            };

            Questionary questionary = _mapper.Map<Questionary>(questionaryDTO);

            Assert.Equal(string.Empty, questionary.Title);
            Assert.False(questionary.IsAvailable);
            Assert.Empty(questionary.Questions);
        }

        [Fact]
        public void Should_Map_Question_To_QuestionDTO()
        {
            Question question = new()
            {
                Text = "Sample Question",
                Alternatives = ["Alt1", "Alt2", "Alt3"],
                CorrectAlternative = 2
            };

            QuestionDTO questionDTO = _mapper.Map<QuestionDTO>(question);

            Assert.Equal(question.Text, questionDTO.Text);
            Assert.Equal(question.Alternatives, questionDTO.Alternatives);
            Assert.Equal(question.CorrectAlternative, questionDTO.CorrectAlternative);
        }

        [Fact]
        public void Should_Map_Null_QuestionDTO_To_Null_Question()
        {
            QuestionDTO? questionDTO = null;

            Question question = _mapper.Map<Question>(questionDTO);

            Assert.Null(question);
        }
    }
}
