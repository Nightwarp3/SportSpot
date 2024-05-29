using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class SubstitutionService : ISubstitutionService
    {
        private readonly IDataService _dataService;

        public SubstitutionService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<Substitution>> GetSubstitutionsByGame(Guid gameId, Guid teamId)
        {
            return await _dataService.GetSubstitutionsByGame(gameId, teamId);
        }

        public async Task<Substitution?> GetSubstitution(Guid substitutionId, Guid gameId, Guid teamId)
        {
            return await _dataService.GetSubstitution(substitutionId, gameId, teamId);
        }

        public async Task<Substitution?> UpsertSubstitution(Substitution substitution, Guid gameId)
        {
            return await _dataService.UpsertSubstitution(substitution, gameId);
        }

        public async Task<bool> DeleteSubstitution(Guid substitutionId, Guid gameId, Guid teamId)
        {
            return await _dataService.DeleteSubstitution(substitutionId, gameId, teamId);
        }
    }
}
