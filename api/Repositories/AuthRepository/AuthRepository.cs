using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class AuthRepository : IAuthRepository
    {
        //Context
        private readonly AppDbContext _dbContext;

        //Constructor & Depedency Injection
        public AuthRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //Methods
        public async Task<User?> FindUser(string? email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
        public async Task AddUser(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}