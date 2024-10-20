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

            List<Questionary> allQuestionaries = await _questionaryService.GetAllQuestionariesAsync();

            Questionary? found = allQuestionaries.FirstOrDefault(q => q.Title == questionaryTitle);

            Assert.NotNull(found);
            Assert.NotEqual(Guid.Empty, found.Id);
            Assert.True(found.IsAvailable);
            Assert.True((DateTime.UtcNow - found.CreatedAt).TotalMilliseconds < 1000);
        }
    }
}
