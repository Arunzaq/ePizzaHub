using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ePizza.Models.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ePizza.Core.Utils
{
    public class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(ValidateUserResponse userResponse)
        {
            string secretkey = _configuration["jwt:Secret"];
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var credentials= new SigningCredentials(securitykey,SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity([
                    new Claim(ClaimTypes.Name,userResponse.Name),
                    new Claim(ClaimTypes.Email,userResponse.Email),
                    new Claim("IsAdmin","true"),
                    new Claim("Roles",JsonSerializer.Serialize(userResponse.Roles))]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["jwt:TokenExpiryInMinutes"])),
                SigningCredentials = credentials,
                Issuer = _configuration["jwt:Issuer"],
                Audience = _configuration["jwt:Audience"]
            };
            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
