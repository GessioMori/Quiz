using Quiz.Models.DTO;
using Quiz.Models.Entities;

namespace Quiz.Shared.Interfaces.Services
{
    public interface IQuestionaryService
    {
        Task CreateQuestionaryAsync(QuestionaryDTO questionaryDTO);
        Task<List<Questionary>> GetAllQuestionariesAsync();
    }
}
