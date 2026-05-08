using Models;

namespace Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}