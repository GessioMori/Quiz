namespace Quiz.Models.Entities
{
    public class QuestionaryAnswer : BaseEntity
    {
        public Guid QuestionaryId { get; set; }
        public List<QuestionAnswer> Answers { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
