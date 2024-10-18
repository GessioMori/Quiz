namespace Quiz.Models.Entities
{
    public class Questionary : BaseEntity
    {
        public string Title { get; set; } = null!;
        public List<Question> Questions { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
