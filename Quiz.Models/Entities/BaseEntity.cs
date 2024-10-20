namespace Quiz.Models.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        protected BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
