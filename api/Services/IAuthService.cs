namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DTOs;

    public interface IAuthService
    {
        string Register(RegisterRequest request);
        string Login(LoginRequest request);
    }
}