using General.DTO;
using General.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace General.Service
{
    public class AuthenticateService: IAuthenticateService
    {
        private readonly TokenDTO token;
        public AuthenticateService(IOptions<TokenDTO> tokenManagement)
        {
            this.token = tokenManagement.Value;
        }
        public bool IsAuthenticated(string tokenId, out string token)
        {
            token = string.Empty;
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name,tokenId)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456123456123456"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken("admin", "admin", claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;

        }
    }
}
