namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DTOs;

    public class AuthService : IAuthService
    {
        public string Login(LoginRequest request)
        {
            return "Login successfully";
        }

        public string Register(RegisterRequest request)
        {
            return "Registered successfully";
        }
    }
}