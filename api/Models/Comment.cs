namespace Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid ProjectTaskId { get; set; }
        public virtual ProjectTask? ProjectTask { get; set; }
    }
}