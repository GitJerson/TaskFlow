using Models;
using DTOs;
namespace Repositories
{
    public interface IAuthRepository
    {
        //Contracts
        Task AddUser(User user);
        Task<User?> FindUser(string? email);

    }
}