using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Services
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JwtConfig> _options;

        public JwtService(IOptions<JwtConfig> options)
        {
            _options = options;
        }


        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret!);

            //things that goes into the token

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name!),
                    new Claim(ClaimTypes.Role, user.Role!),
                }),
                Issuer = _options.Value.Issuer,
                Audience = _options.Value.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_options.Value.ExpiryInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            //create the token object

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //serialize to a string

            return tokenHandler.WriteToken(token);
        }
    }
}