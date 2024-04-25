using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly AuthorizationService _authorizationService;

        public TeamController(ITeamService teamService, AuthorizationService authorizationService)
        {
            _teamService = teamService;
            _authorizationService = authorizationService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetTeam()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                if (team == null)
                {
                    throw new Exception("Unable to retrieve the specified team!");
                }

                return Ok(team);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting team. Request Guid: {requestGuid}, Endpoint: GetTeam, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateTeam(TeamCreationRequest teamCreateRequest)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _teamService.CreateTeam(teamCreateRequest.Team);
                bool success = await _authorizationService.CreateAuthorizedUser(team, teamCreateRequest.Username, teamCreateRequest.Password);

                if (!success)
                {
                    await _teamService.DeleteTeam(team);
                    throw new Exception("Error occurred creating Team and setting Password. Please try again.");
                }

                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest($"Encountered an error while creating team. Request Guid: {requestGuid}, Endpoint: CreateTeam, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPut, Route("")]
        public async Task<IActionResult> UpdateTeam(Team team)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var existingTeam = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                if (existingTeam == null)
                {
                    throw new Exception("Error retrieving team!");
                }

                team.Id = existingTeam.Id;
                var newTeam = await _teamService.UpdateTeam(team);

                return Ok(team);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while Updating the provided Team. Request Guid: {requestGuid}, Endpoint: GetGameTest, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeleteTeam()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                if (team == null)
                {
                    throw new Exception("Error retrieving team!");
                }

                return Ok(await _teamService.DeleteTeam(team));
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting game. Request Guid: {requestGuid}, Endpoint: GetGameTest, HTTPGet, Error: {ex.Message}");
            }
        }
    }
}
