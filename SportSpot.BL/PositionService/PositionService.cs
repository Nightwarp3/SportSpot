using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class PositionService : IPositionService
    {
        private readonly IDataService _dataService;

        public PositionService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<Position>> GetPositionsByTeam(Guid teamId)
        {
            return await _dataService.GetPositionsByTeam(teamId);
        }

        public async Task<Position?> GetPosition(Guid positionId, Guid teamId)
        {
            return await _dataService.GetPosition(positionId, teamId);
        }

        public async Task<Position?> UpsertPosition(Position position)
        {
            return await _dataService.UpsertPosition(position);
        }

        public async Task<bool> DeletePosition(Guid positionId, Guid teamId)
        {
            await _dataService.DeleteSubstitutionsByPosition(positionId, teamId);
            return await _dataService.DeletePosition(positionId, teamId);
        }
    }
}
