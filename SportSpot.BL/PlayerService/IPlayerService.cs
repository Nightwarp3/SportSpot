using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IPlayerService
    {
        Task<bool> DeletePlayer(Guid playerId, Guid teamId);
        Task<Player?> GetPlayer(Guid playerId, Guid teamId);
        Task<IEnumerable<Player>> GetPlayersByTeam(Guid teamId);
        IEnumerable<Player> ShufflePlayers(IEnumerable<Player> players);
        Task<Player?> UpsertPlayer(Player player);
    }
}
