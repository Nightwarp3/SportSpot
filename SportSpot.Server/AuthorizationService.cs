using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportSpot.BL.Models;
using SportSpot.BL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SportSpot.Server
{
    public class AuthorizationService
    {
        public const string Issuer = "SportSpotAPIAuthenticationServer";
        public const string Subject = "SportSpotAPI";
        public const string SigningKey = "zZL5VX4AUzgjhr9kcKyZ3A2luINqUk5txOf7q5HR717FEIkCHmNv23hvi5VULu8";

        private readonly IDataService _dataService;
        private readonly PasswordHasher<string> _hasher = new PasswordHasher<string>();

        public AuthorizationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<Team?> GetAuthenticatedUserTeam(ClaimsPrincipal httpContextUser)
        {
            if (httpContextUser?.Identity?.IsAuthenticated == true)
            {
                var teamUserJson = httpContextUser.Identity.Name;
                var teamUser = JsonSerializer.Deserialize<TeamUser>(teamUserJson);

                if (teamUser != null)
                {
                    return await _dataService.GetTeam(teamUser.TeamId);
                }
            }

            return null;
        }

        public async Task<bool> CreateAuthorizedUser(Team team, string username, string password)
        {
            // If username is provided, create the admin login
            // Otherwise, create the team login
            if (!string.IsNullOrWhiteSpace(username))
            {
                // Verify unique username
                var teamUsers = await _dataService.GetTeamUsers();
                if (teamUsers.Any(x => x.Username == username))
                {
                    throw new UnauthorizedAccessException("Username is already in use. Please choose another.");
                }

                var hashedPassword = HashPassword(username, password);
                var newTeamUser = new TeamUser(team.Id, hashedPassword)
                {
                    Username = username
                };

                return await _dataService.UpsertTeamUser(newTeamUser);
            }
            else
            {
                var hashedTeamPassword = HashPassword(team, password);
                return await _dataService.UpsertTeamUser(new TeamUser(team.Id, hashedTeamPassword));
            }
        }

        public async Task<string> AuthorizeUser(string username, string password)
        {
            // Verify the password resolves to a TeamGuid
            var teamUsers = await _dataService.GetTeamUsers();
            var teamUser = teamUsers.FirstOrDefault(x =>
            {
                if (!string.IsNullOrWhiteSpace(username))
                {
                    if (x.Username == username)
                    {
                        return _hasher.VerifyHashedPassword(username, x.PasswordHash, password) == PasswordVerificationResult.Success;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return _hasher.VerifyHashedPassword(x.TeamId.ToString(), x.PasswordHash, password) == PasswordVerificationResult.Success;
                }
            });

            if (teamUser == null)
            {
                throw new UnauthorizedAccessException("Username or password is incorrect. Please verify and try again.");
            }

            // Wrap it in a JWT Token
            var createdDate = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, createdDate.Subtract(DateTime.UnixEpoch).TotalSeconds.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, JsonSerializer.Serialize(teamUser))
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

        private string HashPassword(Team team, string password)
        {
            return HashPassword(team.Id, password);
        }

        private string HashPassword(TeamUser user, string password)
        {
            return HashPassword(user.TeamId, password);
        }

        private string HashPassword(Guid teamId, string password)
        {
            return _hasher.HashPassword(teamId.ToString(), password);
        }

        private string HashPassword(string username, string password)
        {
            return _hasher.HashPassword(username, password);
        }
    }
}
