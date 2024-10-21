using Quiz.Models.Entities;

namespace Quiz.Shared.Interfaces.Repositories
{
    public interface IQuestionaryRepository
    {
        Task<List<Questionary>> GetAllQuestionariesAsync();
        Task<Questionary?> GetQuestionaryByIdAsync(Guid id);
        Task InsertQuestionaryAsync(Questionary questionary);
        Task InsertQuestionaryAnswerAsync(QuestionaryAnswer questionaryAnswer);
        Task<List<QuestionaryAnswer>> GetQuestionaryAnswersAsync();
    }
}
