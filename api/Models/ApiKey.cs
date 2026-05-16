namespace Models
{
    public class ApiKey
    {
        public Guid Id { get; set; }
        public string? KeyHash { get; set; }
        public string? Label { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRevoked { get; set; }
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
    }
}