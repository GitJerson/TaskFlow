using DTOs;
namespace Services
{
    public interface IApiKeyService
    {
        Task<ICollection<KeyResponseDto>> GetApiKeysForUser(Guid userId);
        Task<KeyResponseDto?> GetApiKeyById(Guid userId, Guid keyId);
        Task<CreateKeyResponseDto> CreateApiKey(Guid userId, CreateKeyDto request);
        Task<string> UpdateApiKey(Guid userId, Guid keyId, UpdateKeyDto request);
        Task<string> RevokeApiKey(Guid userId, Guid keyId);
    }
}