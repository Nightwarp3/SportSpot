using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly IPlayerService _playerService;

        public PlayerController(AuthorizationService authorizationService, IPlayerService playerService)
        {
            _authorizationService = authorizationService;
            _playerService = playerService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetPlayer(Guid playerGuid)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var player = await _playerService.GetPlayer(playerGuid, team.Id);

                return Ok(player);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting player. Request Guid: {requestGuid}, Endpoint: GetPlayer, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> UpsertPlayer([FromBody] Player player)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                player.TeamId = team.Id;

                var newPlayer = await _playerService.UpsertPlayer(player);

                return Ok(newPlayer);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error updating or inserting player. Request Guid: {requestGuid}, Endpoint: UpsertPlayer, HTTPPost, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeletePlayer([FromBody] Player player)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                player.TeamId = team.Id;

                var playerDeleted = await _playerService.DeletePlayer(player.Id, team.Id);

                return Ok(playerDeleted);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error deleting player. Request Guid: {requestGuid}, Endpoint: DeletePlayer, HTTPDelete, Error: {ex.Message}");
            }
        }
    }
}
