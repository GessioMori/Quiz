using Quiz.Models.Entities;
using Quiz.Shared.Interfaces.Repositories;

namespace Quiz.Shared.InMemoryRepositories
{
    public class InMemoryQuestionaryRepository : IQuestionaryRepository
    {
        private readonly List<Questionary> _questionaries;

        public InMemoryQuestionaryRepository()
        {
            this._questionaries = [];
        }

        public Task<List<Questionary>> GetAllQuestionariesAsync()
        {
            return Task.FromResult(this._questionaries.ToList());
        }

        public Task InsertQuestionaryAsync(Questionary questionary)
        {
            questionary.Id = Guid.NewGuid();
            this._questionaries.Add(questionary);
            return Task.CompletedTask;
        }
    }
}
