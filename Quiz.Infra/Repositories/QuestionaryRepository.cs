using MongoDB.Driver;
using Quiz.Models.Entities;
using Quiz.Shared.Interfaces.Repositories;

namespace Quiz.Infra.Repositories
{
    public class QuestionaryRepository : IQuestionaryRepository
    {
        private readonly IMongoCollection<Questionary> _questionaryCollection;

        public QuestionaryRepository(IMongoDatabase database)
        {
            this._questionaryCollection = database.GetCollection<Questionary>("questionary");
        }

        public async Task<List<Questionary>> GetAllQuestionariesAsync()
        {
            return await this._questionaryCollection.Find(FilterDefinition<Questionary>.Empty).ToListAsync();
        }

        public async Task InsertQuestionaryAsync(Questionary questionary)
        {
            await this._questionaryCollection.InsertOneAsync(questionary);
        }
    }
}
