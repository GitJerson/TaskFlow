using Models;

namespace Repositories
{
    public interface IApiKeyRepository
    {
        Task<ICollection<ApiKey>> GetAllKeysAsync(Guid userId);
        Task<ICollection<ApiKey>> GetKeysAsync();
        Task<ApiKey?> GetKeyByIdAsync(Guid userId, Guid keyId);
        Task CreateKeyAsync(ApiKey key);
        Task UpdateKeyAsync(ApiKey key);
    }
}