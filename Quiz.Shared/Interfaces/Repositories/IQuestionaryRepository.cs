using Quiz.Models.Entities;

namespace Quiz.Shared.Interfaces.Repositories
{
    public interface IQuestionaryRepository
    {
        Task<List<Questionary>> GetAllQuestionariesAsync();
        Task InsertQuestionaryAsync(Questionary questionary);
    }
}
