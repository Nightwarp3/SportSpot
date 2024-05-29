using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IPositionService
    {
        Task<bool> DeletePosition(Guid positionId, Guid teamId);
        Task<Position?> GetPosition(Guid positionId, Guid teamId);
        Task<IEnumerable<Position>> GetPositionsByTeam(Guid teamId);
        Task<Position?> UpsertPosition(Position position);
    }
}
