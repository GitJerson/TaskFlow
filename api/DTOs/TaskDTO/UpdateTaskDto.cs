using Models;
namespace Dtos
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Guid? AssignedUserId { get; set; }
    }
}