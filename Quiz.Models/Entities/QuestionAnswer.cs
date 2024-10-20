namespace Quiz.Models.Entities
{
    public class QuestionAnswer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public int Answer;
    }
}
