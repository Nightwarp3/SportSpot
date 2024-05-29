using Microsoft.AspNetCore.Mvc;
using SportSpot.BL.Models;
using SportSpot.BL.Services;

namespace SportSpot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubstitutionController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly ISubstitutionService _substitutionService;

        public SubstitutionController(AuthorizationService authorizationService, ISubstitutionService substitutionService)
        {
            _authorizationService = authorizationService;
            _substitutionService = substitutionService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetSubstitution(Guid substitutionGuid, Guid gameId)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var substitution = await _substitutionService.GetSubstitution(substitutionGuid, gameId, team.Id);

                return Ok(substitution);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error while getting substitution. Request Guid: {requestGuid}, Endpoint: GetSubstitution, HTTPGet, Error: {ex.Message}");
            }
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> UpsertSubstitution([FromBody] Substitution substitution)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var newSubstitution = await _substitutionService.UpsertSubstitution(substitution, substitution.GameId);

                return Ok(newSubstitution);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error updating or inserting substitution. Request Guid: {requestGuid}, Endpoint: UpsertSubstitution, HTTPPost, Error: {ex.Message}");
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> DeleteSubstitution([FromBody] Substitution substitution)
        {
            Guid requestGuid = Guid.NewGuid();

            try
            {
                var team = await _authorizationService.GetAuthenticatedUserTeam(HttpContext.User);

                var substitutionDeleted = await _substitutionService.DeleteSubstitution(substitution.Id, substitution.GameId, team.Id);

                return Ok(substitutionDeleted);
            }
            catch (Exception ex)
            {
                // Log the exception
                return BadRequest($"Encountered an error deleting substitution. Request Guid: {requestGuid}, Endpoint: DeleteSubstitution, HTTPDelete, Error: {ex.Message}");
            }
        }
    }
}
