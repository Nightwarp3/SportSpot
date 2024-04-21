using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class PlayerService : IPlayerService
    {
        public IEnumerable<Player> ShufflePlayers(IEnumerable<Player> players)
        {
            // randomize the players
            var random = new Random();
            List<Player> randomizedList = players.ToList();
            int n = randomizedList.Count();
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Player player = randomizedList[k];
                randomizedList[k] = randomizedList[n];
                randomizedList[n] = player;
            }

            return randomizedList;
        }
    }
}
