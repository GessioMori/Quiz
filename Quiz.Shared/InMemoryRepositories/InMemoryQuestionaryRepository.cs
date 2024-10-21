using Quiz.Models.Entities;
using Quiz.Shared.Interfaces.Repositories;

namespace Quiz.Shared.InMemoryRepositories
{
    public class InMemoryQuestionaryRepository : IQuestionaryRepository
    {
        private readonly List<Questionary> _questionaries;
        private readonly List<QuestionaryAnswer> _questionaryAnswers;

        public InMemoryQuestionaryRepository()
        {
            this._questionaries = [];
            this._questionaryAnswers = [];
        }

        public Task<List<Questionary>> GetAllQuestionariesAsync()
        {
            return Task.FromResult(this._questionaries.ToList());
        }

        public Task<List<QuestionaryAnswer>> GetQuestionaryAnswersAsync()
        {
            return Task.FromResult(this._questionaryAnswers.ToList());
        }

        public Task<Questionary?> GetQuestionaryByIdAsync(Guid id)
        {
            return Task.FromResult(this._questionaries.FirstOrDefault(q => q.Id == id));
        }

        public Task InsertQuestionaryAnswerAsync(QuestionaryAnswer questionaryAnswer)
        {
            this._questionaryAnswers.Add(questionaryAnswer);
            return Task.CompletedTask;
        }

        public Task InsertQuestionaryAsync(Questionary questionary)
        {
            this._questionaries.Add(questionary);
            return Task.CompletedTask;
        }
    }
}
