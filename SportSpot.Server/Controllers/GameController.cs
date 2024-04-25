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
        private readonly ITeamService _teamService;
        private readonly IGameService _gameService;

        public GameController(AuthorizationService authorizationService, ITeamService teamService, IGameService gameService)
        {
            _authorizationService = authorizationService;
            _teamService = teamService;
            _gameService = gameService;
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
                return BadRequest($"Encountered an error while getting game. Request Guid: {requestGuid}, Endpoint: GetGameTest, HTTPGet, Error: {ex.Message}");
            }
        }
    }
}
