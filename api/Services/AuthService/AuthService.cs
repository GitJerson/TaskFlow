namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DTOs;
    using Repositories;
    using Models;
    public class AuthService : IAuthService
    {
        //Repository
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;

        //Consturctor & Dependency Injection
        public AuthService(IAuthRepository authRepository, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        //Methods
        public async Task<string> GoogleLoginAsync(string email, string name)
        {
            var user = await _authRepository.FindUser(email);

            if(user != null)
            {
                var token = _jwtService.GenerateJwtToken(user);
                return token;
            }

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = string.Empty,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };
            await _authRepository.AddUser(newUser);
            var nToken = _jwtService.GenerateJwtToken(newUser);
            return nToken;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _authRepository.FindUser(request.Email);

            if(user != null)
            {
                var isMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
                if(isMatch)
                    return _jwtService.GenerateJwtToken(user);
            }

            return null!;
        }

        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var isExist = await _authRepository.FindUser(request.Email);

            if(isExist != null)
                return "Email already exists";

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.Password,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,

            };

            await _authRepository.AddUser(user);

            return "User created successfully";
        }
    }
}