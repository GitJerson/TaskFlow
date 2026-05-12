using Models;
namespace Dtos
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? AssignedTo{ get; set; }
        
    }
}