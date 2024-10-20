namespace Quiz.Models.DTO
{
    public class QuestionaryAnswerDTO
    {
        public Guid QuestionaryId { get; set; }
        public List<QuestionAnswerDTO> Answers { get; set; } = null!;
    }
}
