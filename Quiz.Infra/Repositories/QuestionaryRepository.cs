using MongoDB.Driver;
using Quiz.Models.Entities;
using Quiz.Shared.Interfaces.Repositories;

namespace Quiz.Infra.Repositories
{
    public class QuestionaryRepository : IQuestionaryRepository
    {
        private readonly IMongoCollection<Questionary> _questionaryCollection;
        private readonly IMongoCollection<QuestionaryAnswer> _questionaryAnswerCollection;

        public QuestionaryRepository(IMongoDatabase database)
        {
            this._questionaryCollection = database.GetCollection<Questionary>("questionary");
            this._questionaryAnswerCollection = database.GetCollection<QuestionaryAnswer>("questionaryAnswer");
        }

        public async Task<List<Questionary>> GetAllQuestionariesAsync()
        {
            return await this._questionaryCollection.Find(FilterDefinition<Questionary>.Empty).ToListAsync();
        }

        public async Task InsertQuestionaryAsync(Questionary questionary)
        {
            await this._questionaryCollection.InsertOneAsync(questionary);
        }

        public async Task InsertQuestionaryAnswerAsync(QuestionaryAnswer questionaryAnswer)
        {
            Questionary questionary = await this._questionaryCollection
                .Find(q => q.Id == questionaryAnswer.QuestionaryId)
                .FirstOrDefaultAsync();

            if (questionary == null)
            {
                throw new ArgumentException();
            }

            await this._questionaryAnswerCollection.InsertOneAsync(questionaryAnswer);
        }

        public async Task<List<QuestionaryAnswer>> GetQuestionaryAnswersAsync()
        {
            return await this._questionaryAnswerCollection
                .Find(FilterDefinition<QuestionaryAnswer>.Empty)
                .ToListAsync();
        }
    }
}
