using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IGameService _gameService;

        public GameController(ITeamService teamService, IGameService gameService)
        {
            _teamService = teamService;
            _gameService = gameService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetGameTest()
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = _teamService.GetTeam(string.Empty);
                var games = _gameService.GetGamesByTeam(team.Id);
                var game = games.FirstOrDefault();
                if (game == null)
                {
                    throw new Exception("Unable to retrieve the specified game!");
                }

                game.Substitutions = _gameService.GenerateSubstitutions(game.AttendingPlayers, team.Positions, team.Rotations);

                return Ok(game);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting game. Request Guid: {requestGuid}, Endpoint: GetGameTest, HTTPGet, Error: {ex.Message}");
            }
        }
    }
}
