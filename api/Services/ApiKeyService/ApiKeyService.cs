using System.Security.Cryptography;
using DTOs;
using Repositories;
namespace Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyService(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<CreateKeyResponseDto> CreateApiKey(Guid userId, CreateKeyDto request)
        {
            var rawKey = Guid.NewGuid().ToString("N"); // Generate a random key
            var apiKey = new Models.ApiKey
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                KeyHash = BCrypt.Net.BCrypt.HashPassword(rawKey), // Store only the hash of the key
                Label = request.Label,
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };
            await _apiKeyRepository.CreateKeyAsync(apiKey);
            var response = new CreateKeyResponseDto
            {
                Id = apiKey.Id,
                Key = rawKey,
                Label = apiKey.Label,
                CreatedAt = apiKey.CreatedAt
            };
            return response;
        }

        public async Task<KeyResponseDto?> GetApiKeyById(Guid userId, Guid keyId)
        {
            var apiKey = await _apiKeyRepository.GetKeyByIdAsync(userId, keyId);
            if (apiKey == null) return null;

            return new KeyResponseDto
            {
                Id = apiKey.Id,
                Label = apiKey.Label,
                CreatedAt = apiKey.CreatedAt
            };
        }

        public async Task<ICollection<KeyResponseDto>> GetApiKeysForUser(Guid userId)
        {
            var apiKeys = await _apiKeyRepository.GetAllKeysAsync(userId);
            return apiKeys.Select(apiKey => new KeyResponseDto
            {
                Id = apiKey.Id,
                Label = apiKey.Label,
                CreatedAt = apiKey.CreatedAt
            }).ToList();
        }

        public async Task<string> RevokeApiKey(Guid userId, Guid keyId)
        {
            var apiKey = await _apiKeyRepository.GetKeyByIdAsync(userId, keyId);
            if (apiKey == null) 
                return "API key not found";

            apiKey.IsRevoked = true;
            await _apiKeyRepository.UpdateKeyAsync(apiKey);

            return "API key revoked successfully";
        }

        public async Task<string> UpdateApiKey(Guid userId, Guid keyId, UpdateKeyDto request)
        {
            var apiKey = await _apiKeyRepository.GetKeyByIdAsync(userId, keyId);
            if (apiKey == null) 
            return "API key not found";

            apiKey.Label = request.Label;
            await _apiKeyRepository.UpdateKeyAsync(apiKey);

            return "API key updated successfully";
        }
    }
}