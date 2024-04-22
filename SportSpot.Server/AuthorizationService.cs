using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportSpot.BL.Models;
using SportSpot.BL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportSpot.Server
{
    public class AuthorizationService
    {
        public const string Issuer = "SportSpotAPIAuthenticationServer";
        public const string Subject = "SportSpotAPI";
        public const string SigningKey = "zZL5VX4AUzgjhr9kcKyZ3A2luINqUk5txOf7q5HR717FEIkCHmNv23hvi5VULu8";

        private readonly IDataService _dataService;

        public AuthorizationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public string AuthorizeUser(string password)
        {
            var hasher = new PasswordHasher<TeamUser>();

            // Verify the password resolves to a TeamGuid
            var teamUsers = _dataService.GetTeams();
            var teamUser = teamUsers.FirstOrDefault(x =>
            {
                var hashedPassword = hasher.HashPassword(x, password);
                return hasher.VerifyHashedPassword(x, hashedPassword, password) == PasswordVerificationResult.Success;
            });

            if (teamUser == null)
            {
                // TODO: If we ever switch to using usernames, need to include the error here.
                throw new UnauthorizedAccessException("Invalid password. Please verify and try again.");
            }

            // Wrap it in a JWT Token
            var createdDate = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, createdDate.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, teamUser.TeamId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Issuer,
                Issuer,
                claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
