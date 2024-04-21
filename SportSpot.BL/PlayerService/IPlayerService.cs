using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public interface IPlayerService
    {
        public IEnumerable<Player> ShufflePlayers(IEnumerable<Player> players);
    }
}
