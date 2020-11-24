using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Authentication
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthRepository _authRepo;
        private readonly string _signingKey;

        public AuthManager(IAuthRepository authRepo, string signingKey)
        {
            _authRepo = authRepo;
            _signingKey = signingKey;
        }

        public string Authenticate(string username, string password, UserRoles role)
        {
            string token = null;
            if (_authRepo.AuthenticateUser(username, password, role))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_signingKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, RoleDescriptor.Describe(role))
                    }),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha512Signature),
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(90)
                };
                token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            }
            return token;
        }
    }
}