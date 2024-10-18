namespace Quiz.Models.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; } = null!;
        public List<string> Alternatives { get; set; } = null!;
        public int CorrectAlternative { get; set; }
    }
}
