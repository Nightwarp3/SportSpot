using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IGameService
    {
        List<Substitution> GenerateSubstitutions(IEnumerable<Player> players, IEnumerable<Position> positions, IEnumerable<Rotation> rotations);
        public IEnumerable<Game> GetGamesByTeam(Guid teamId);
    }
}
