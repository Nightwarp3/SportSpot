using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly IGameService _gameService;
        private readonly ISubstitutionService _substitutionService;

        public GameController(AuthorizationService authorizationService, IGameService gameService, ISubstitutionService substitutionService)
        {
            _authorizationService = authorizationService;
            _gameService = gameService;
            _substitutionService = substitutionService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetGame(Guid gameGuid)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var game = await _gameService.GetGame(gameGuid, team.Id);

                return Ok(game);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting game. Request Guid: {requestGuid}, Endpoint: GetGame, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> UpsertGame([FromBody] Game game)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                game.TeamId = team.Id;

                var newGame = await _gameService.UpsertGame(game);

                return Ok(newGame);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error updating or inserting game. Request Guid: {requestGuid}, Endpoint: UpsertGame, HTTPPost, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeleteGame([FromBody] Game game)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                game.TeamId = team.Id;

                var gameDeleted = await _gameService.DeleteGame(game.Id, team.Id);

                return Ok(gameDeleted);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error deleting game. Request Guid: {requestGuid}, Endpoint: DeleteGame, HTTPDelete, Error: {ex.Message}");
            }
        }

        [HttpGet, Route("{gameId}/Substitution")]
        public async Task<IActionResult> GetGameSubstitutions(Guid gameId)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);
                var substitutions = await _substitutionService.GetSubstitutionsByGame(gameId, team.Id);

                return Ok(substitutions);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting substitutions for game. Request Guid: {requestGuid}, Endpoint: GetGameSubstitutions, HTTPGet, Error: {ex.Message}");
            }
        }
    }
}
