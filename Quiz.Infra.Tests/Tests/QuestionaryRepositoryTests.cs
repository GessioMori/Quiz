using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Quiz.Models.Entities;
using Quiz.Shared.Interfaces.Repositories;
using Xunit;

namespace Quiz.Infra.Tests.Tests
{
    [CollectionDefinition("MongoDbCollection")]
    public class QuestionaryRepositoryTests : IClassFixture<MongoDbFixture>
    {
        private readonly IMongoDatabase _database;
        private readonly IQuestionaryRepository _questionaryRepository;

        public QuestionaryRepositoryTests(MongoDbFixture dbFixture)
        {
            this._database = dbFixture.ServiceProvider.GetRequiredService<IMongoDatabase>();
            this._questionaryRepository = dbFixture.ServiceProvider.GetRequiredService<IQuestionaryRepository>();
        }


        [Fact]
        public async Task InsertQuestionaryAsync_ShouldInsertAndRetrieveQuestionary()
        {
            string questionaryTitle = Guid.NewGuid().ToString();

            Questionary questionary = new()
            {
                Title = questionaryTitle,
                Questions =
                [
                    new Question
                    {
                        Id = Guid.NewGuid(),
                        Text = "Sample Question?",
                        Alternatives = ["A", "B", "C", "D"],
                        CorrectAlternative = 1
                    }
                ],
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            await this._questionaryRepository.InsertQuestionaryAsync(questionary);
            List<Questionary> result = await this._questionaryRepository.GetAllQuestionariesAsync();

            Questionary? found = result.FirstOrDefault(q => q.Title == questionaryTitle);

            Assert.NotNull(found);
            Assert.Equal(questionaryTitle, found.Title);
        }
    }
}
