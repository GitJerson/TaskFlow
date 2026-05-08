using Models;
using DTOs;
namespace Repositories
{
    public interface IAuthRepository
    {
        Task SaveUser(User user);
        Task<User?> FindUser(string? email);

    }
}