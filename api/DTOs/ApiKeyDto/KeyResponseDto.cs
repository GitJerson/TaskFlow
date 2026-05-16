namespace DTOs
{
    public class KeyResponseDto
    {
        public Guid Id { get; set; }
        public string? Label { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}