using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IDataService _dataService;

        public PlayerService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeam(Guid teamId)
        {
            return await _dataService.GetPlayersByTeam(teamId);
        }

        public async Task<Player?> GetPlayer(Guid playerId, Guid teamId)
        {
            return await _dataService.GetPlayer(playerId, teamId);
        }

        public async Task<Player?> UpsertPlayer(Player player)
        {
            return await _dataService.UpsertPlayer(player);
        }

        public async Task<bool> DeletePlayer(Guid playerId, Guid teamId)
        {
            await _dataService.DeleteSubstitutionsByPlayer(playerId, teamId);
            return await _dataService.DeletePlayer(playerId, teamId);
        }

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
