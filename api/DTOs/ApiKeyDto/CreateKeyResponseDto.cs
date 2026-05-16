namespace DTOs
{
    public class CreateKeyResponseDto
    {
        public Guid Id { get; set; }
        public string Key { get; set; } = null!;
        public string? Label { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}