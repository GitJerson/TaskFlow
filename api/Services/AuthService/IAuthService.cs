namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DTOs;
    using Models;
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string>  LoginAsync(LoginRequest request);
        Task<string> GoogleLoginAsync(string email, string name);
    }
}