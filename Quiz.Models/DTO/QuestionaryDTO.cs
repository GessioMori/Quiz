namespace Quiz.Models.DTO
{
    public class QuestionaryDTO
    {
        public string Title { get; set; } = null!;
        public List<QuestionDTO> Questions { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
