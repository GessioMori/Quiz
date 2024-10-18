namespace Quiz.Models.DTO
{
    public class QuestionDTO
    {
        public string Text { get; set; } = null!;
        public List<string> Alternatives { get; set; } = null!;
        public int CorrectAlternative { get; set; }
    }
}
