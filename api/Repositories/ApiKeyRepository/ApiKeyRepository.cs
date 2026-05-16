using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly AppDbContext _context;

        public ApiKeyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateKeyAsync(ApiKey key)
        {
            await _context.ApiKeys.AddAsync(key);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ApiKey>> GetAllKeysAsync(Guid userId)
        {
            var key = await _context.ApiKeys
                        .Where(k => k.UserId == userId)
                        .ToListAsync();

            return key;
        }

        public async Task<ApiKey?> GetKeyByIdAsync(Guid userId, Guid keyId)
        {
            var key = await _context.ApiKeys
                        .Where(k => k.UserId == userId && k.Id == keyId)
                        .FirstOrDefaultAsync();
            return key;
        }

        public async Task<ICollection<ApiKey>> GetKeysAsync()
        {
            var keys = await _context.ApiKeys.Where(k => k.IsRevoked == false).ToListAsync();

            return keys;
        }

        public async Task UpdateKeyAsync(ApiKey key)
        {
            _context.ApiKeys.Update(key);
            await _context.SaveChangesAsync();
        }
    }
}