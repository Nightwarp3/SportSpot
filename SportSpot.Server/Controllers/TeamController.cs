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
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;
        private readonly IPositionService _positionService;
        private readonly IRotationService _rotationService;
        private readonly AuthorizationService _authorizationService;

        public TeamController(
            ITeamService teamService,
            IGameService gameService,
            IPlayerService playerService,
            IPositionService positionService,
            IRotationService rotationService,
            AuthorizationService authorizationService
        )
        {
            _teamService = teamService;
            _authorizationService = authorizationService;
            _gameService = gameService;
            _playerService = playerService;
            _positionService = positionService;
            _rotationService = rotationService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetTeam()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

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
                bool success = await _authorizationService.CreateAuthorizedUser(team, teamCreateRequest.Username.ToLower(), teamCreateRequest.Password);

                if (!success)
                {
                    await _teamService.DeleteTeam(team);
                    throw new Exception("Error occurred creating Team and setting Password. Please try again.");
                }

                // Don't return the team, we'll want the user to reauthenticate
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Encountered an error while creating team. Request Guid: {requestGuid}, Endpoint: CreateTeam, HttpPost, Error: {ex.Message}");
            }
        }

        [HttpPut, Route("")]
        public async Task<IActionResult> UpdateTeam(Team team)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var existingTeam = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                team.Id = existingTeam.Id;
                var newTeam = await _teamService.UpdateTeam(team);

                return Ok(team);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while updating the provided Team. Request Guid: {requestGuid}, Endpoint: UpdateTeam, HttpPut, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeleteTeam()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                return Ok(await _teamService.DeleteTeam(team));
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while deleting game. Request Guid: {requestGuid}, Endpoint: DeleteTeam, HTTPDelete, Error: {ex.Message}");
            }
        }

        [HttpGet, Route("Game")]
        public async Task<IActionResult> GetTeamGames()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                var games = await _gameService.GetGamesByTeam(team.Id);

                return Ok(games);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting games for team. Request Guid: {requestGuid}, Endpoint: GetTeamGames, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpGet, Route("Player")]
        public async Task<IActionResult> GetTeamPlayers()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                var players = await _playerService.GetPlayersByTeam(team.Id);

                return Ok(players);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting players for team. Request Guid: {requestGuid}, Endpoint: GetTeamPlayers, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpGet, Route("Position")]
        public async Task<IActionResult> GetTeamPositions()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                var positions = await _positionService.GetPositionsByTeam(team.Id);

                return Ok(positions);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting positions for team. Request Guid: {requestGuid}, Endpoint: GetTeamPositions, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpGet, Route("Rotation")]
        public async Task<IActionResult> GetTeamRotations()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                var rotations = await _rotationService.GetRotationsByTeam(team.Id);

                return Ok(rotations);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting rotations for team. Request Guid: {requestGuid}, Endpoint: GetTeamRotations, HTTPGet, Error: {ex.Message}");
            }
        }
    }
}
