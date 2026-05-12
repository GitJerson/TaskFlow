namespace Models
{
    using System;

    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public Guid? AssignedUserId { get; set; }
        public virtual User? AssignedUser { get; set; }
        public Priority Priority { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}