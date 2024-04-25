using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IGameService
    {
        Task<bool> DeleteGame(Guid gameId);
        Task<List<Substitution>> GenerateSubstitutions(IEnumerable<Player> players, IEnumerable<Position> positions, IEnumerable<Rotation> rotations);
        Task<Game> GetGame(Guid gameId, Guid teamId);
        Task<IEnumerable<Game>> GetGamesByTeam(Guid teamId);
        Task<Game?> UpsertGame(Game game);
    }
}
