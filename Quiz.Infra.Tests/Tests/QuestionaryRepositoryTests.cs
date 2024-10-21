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
            (string questionaryTitle, Questionary questionary) = CreateQuestionary();

            await this._questionaryRepository.InsertQuestionaryAsync(questionary);
            List<Questionary> result = await this._questionaryRepository.GetAllQuestionariesAsync();

            Questionary? found = result.FirstOrDefault(q => q.Title == questionaryTitle);

            Assert.NotNull(found);
            Assert.NotEqual(Guid.Empty, found.Id);
            Assert.Equal(questionaryTitle, found.Title);
        }

        [Fact]
        public async Task InsertQuestionaryAnswerAsync_ShouldInsertAndRetrieveQuestionaryAnswer()
        {
            (string _, Questionary questionary) = CreateQuestionary();
            await this._questionaryRepository.InsertQuestionaryAsync(questionary);

            const int answerIndex = 2;

            QuestionaryAnswer questionaryAnswer = new()
            {
                QuestionaryId = questionary.Id,
                CreatedAt = DateTime.UtcNow,
                Answers = [new QuestionAnswer() { Answer = answerIndex, QuestionId = questionary.Questions[0].Id }]
            };

            await this._questionaryRepository.InsertQuestionaryAnswerAsync(questionaryAnswer);

            List<QuestionaryAnswer> result = await this._questionaryRepository.GetQuestionaryAnswersAsync();

            QuestionaryAnswer? found = result.FirstOrDefault(qa => qa.QuestionaryId == questionary.Id);

            Assert.Multiple(() =>
            {
                Assert.NotNull(found);
                Assert.NotEqual(Guid.Empty, found.Id);

                QuestionAnswer? questionAnswer = found.Answers
                    .FirstOrDefault(a => a.QuestionId == questionary.Questions[0].Id);

                Assert.NotNull(questionAnswer);
                Assert.Equal(answerIndex, questionAnswer.Answer);
            });
        }

        private (string, Questionary) CreateQuestionary()
        {
            string questionaryTitle = Guid.NewGuid().ToString();

            Questionary questionary = new()
            {
                Title = questionaryTitle,
                Questions =
                [
                    new Question
                    {
                        Text = "Sample Question?",
                        Alternatives = ["A", "B", "C", "D"],
                        CorrectAlternative = 1
                    }
                ],
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            return (questionaryTitle, questionary);
        }
    }
}
