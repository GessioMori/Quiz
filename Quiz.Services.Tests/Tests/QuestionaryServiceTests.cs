using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Models.DTO;
using Quiz.Models.Entities;
using Quiz.Services.Services;
using Quiz.Shared.Interfaces.Repositories;
using Quiz.Shared.Interfaces.Services;
using Xunit;

namespace Quiz.Services.Tests.Tests
{
    public class QuestionaryServiceTests : IClassFixture<QuestionaryServiceFixture>
    {
        private readonly IQuestionaryService _questionaryService;
        private readonly IQuestionaryRepository _questionaryRepository;
        private readonly IMapper _mapper;

        public QuestionaryServiceTests(QuestionaryServiceFixture fixture)
        {
            this._questionaryRepository = fixture.ServiceProvider.GetRequiredService<IQuestionaryRepository>();
            this._mapper = fixture.ServiceProvider.GetRequiredService<IMapper>();
            this._questionaryService = new QuestionaryService(this._questionaryRepository, this._mapper);
        }

        [Fact]
        public async Task CreateQuestionaryAsync_ShouldInsertQuestionary()
        {
            string questionaryTitle = Guid.NewGuid().ToString();

            QuestionaryDTO questionaryDTO = new()
            {
                Title = questionaryTitle,
                Questions = [
                    new QuestionDTO()
                    {
                        Text = "Sample Question?",
                        Alternatives = ["A", "B", "C", "D"],
                        CorrectAlternative = 1
                    }
                ],
            };

            await this._questionaryService.CreateQuestionaryAsync(questionaryDTO);

            List<Questionary> allQuestionaries = await this._questionaryService.GetAllQuestionariesAsync();

            Questionary? found = allQuestionaries.FirstOrDefault(q => q.Title == questionaryTitle);

            Assert.NotNull(found);
            Assert.NotEqual(Guid.Empty, found.Id);
            Assert.True(found.IsAvailable);
            Assert.True((DateTime.UtcNow - found.CreatedAt).TotalMilliseconds < 1000);
        }

        [Fact]
        public async Task CreateQuestionaryAnswerAsync_ShouldMapAndInsertWhenQuestionaryExists()
        {
            string questionaryTitle = Guid.NewGuid().ToString();

            QuestionaryDTO questionaryDTO = new()
            {
                Title = questionaryTitle,
                Questions = [
                    new QuestionDTO()
                    {
                        Text = "1",
                        Alternatives = ["A", "B", "C", "D"],
                        CorrectAlternative = 1
                    },
                    new QuestionDTO()
                    {
                        Text = "2",
                        Alternatives = ["A", "B", "C", "D"],
                        CorrectAlternative = 3
                    }
                ],
            };

            await this._questionaryService.CreateQuestionaryAsync(questionaryDTO);

            List<Questionary> allQuestionaries = await _questionaryService.GetAllQuestionariesAsync();

            Questionary? createdQuestionary = allQuestionaries.FirstOrDefault(q => q.Title == questionaryTitle);

            Assert.NotNull(createdQuestionary);

            QuestionaryAnswerDTO questionaryAnswerDTO = new()
            {
                QuestionaryId = createdQuestionary.Id,
                Answers = [
                    new QuestionAnswerDTO(){
                        QuestionId = createdQuestionary.Questions.First(q => q.Text == "1").Id,
                        Answer = 2
                    },
                    new QuestionAnswerDTO(){
                        QuestionId = createdQuestionary.Questions.First(q => q.Text == "2").Id,
                        Answer = 3
                    },
                ],
            };

            await this._questionaryService.CreateQuestionaryAnswerAsync(questionaryAnswerDTO);

            List<QuestionaryAnswer> allQuestionaryAnswers = await this._questionaryService.GetQuestionaryAnswersAsync();

            QuestionaryAnswer? createdQuestionaryAnswer = allQuestionaryAnswers
                .FirstOrDefault(q => q.QuestionaryId == createdQuestionary.Id);

            Assert.NotNull(createdQuestionaryAnswer);
            Assert.Equal(2, createdQuestionaryAnswer.Answers[0].Answer);
            Assert.Equal(3, createdQuestionaryAnswer.Answers[1].Answer);
        }

        [Fact]
        public async Task CreateQuestionaryAnswerAsync_ShouldThrowArgumentExceptionWhenQuestionaryNotFound()
        {
            QuestionaryAnswerDTO questionaryAnswerDTO = new()
            {
                QuestionaryId = Guid.NewGuid(),
                Answers = [
                    new QuestionAnswerDTO(){
                            QuestionId = Guid.NewGuid(),
                            Answer = 2
                        }
                ],
            };

            await Assert.ThrowsAsync<ArgumentException>(() => this._questionaryService
                .CreateQuestionaryAnswerAsync(questionaryAnswerDTO));
        }

        [Fact]
        public async Task CreateQuestionaryAnswerAsync_ShouldThrowArgumentExceptionWhenQuestionNotFound()
        {
            string questionaryTitle = Guid.NewGuid().ToString();

            QuestionaryDTO questionaryDTO = new()
            {
                Title = questionaryTitle,
                Questions = [
                    new QuestionDTO()
                    {
                        Text = "Sample Question?",
                        Alternatives = ["A", "B", "C", "D"],
                        CorrectAlternative = 1
                    }
                ],
            };

            await this._questionaryService.CreateQuestionaryAsync(questionaryDTO);

            List<Questionary> allQuestionaries = await this._questionaryService.GetAllQuestionariesAsync();

            Questionary? createdQuestionary = allQuestionaries.FirstOrDefault(q => q.Title == questionaryTitle);

            Assert.NotNull(createdQuestionary);

            QuestionaryAnswerDTO questionaryAnswerDTO = new()
            {
                QuestionaryId = createdQuestionary.Id,
                Answers = [
                    new QuestionAnswerDTO(){
                        QuestionId = Guid.NewGuid(),
                        Answer = 2
                    }
                ],
            };

            await Assert.ThrowsAsync<ArgumentException>(() => this._questionaryService
                .CreateQuestionaryAnswerAsync(questionaryAnswerDTO));
        }
    }
}
