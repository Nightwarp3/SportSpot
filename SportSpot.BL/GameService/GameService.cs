using SportSpot.BL.Models;

namespace SportSpot.BL.Services
{
    public class GameService : IGameService
    {
        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;

        public GameService(ITeamService teamService, IPlayerService playerService)
        {
            _teamService = teamService;
            _playerService = playerService;
        }

        public List<Substitution> GenerateSubstitutions(IEnumerable<Player> players, IEnumerable<Position> positions, IEnumerable<Rotation> rotations)
        {
            List<Substitution> substitutions = new List<Substitution>();

            // For each rotation, loop through each position, assign a player
            Dictionary<Player, int> assignedCount = new Dictionary<Player, int>();
            for (int index = 0; index < rotations.Count(); index++)
            {
                var rotation = rotations.ElementAt(index);

                // Populate the limited positions first
                foreach (var position in positions.Where(x => x.NumberAllowed != null))
                {
                    int numberAssigned = 0;

                    while (numberAssigned < position.NumberAllowed)
                    {
                        var randomizedPlayers = _playerService.ShufflePlayers(players);
                        // Find all the players assigned to this rotation
                        var playersAlreadyAssigned = substitutions.Where(x => x.Rotation == rotation).Select(x => x.Player);

                        // Find all the currently assigned positions
                        var playersAssignedToThisPosition = substitutions.Where(x => x.Position == position).Select(x => x.Player);

                        // Find the first player in the randomized list that hasn't been assigned to this position yet in the game
                        var player = randomizedPlayers.FirstOrDefault(x => !playersAlreadyAssigned.Contains(x)
                                                                            && !playersAssignedToThisPosition.Contains(x));

                        if (player == null)
                        {
                            // Find all the players that have been assigned less ofter
                            var underAssignedPlayers = randomizedPlayers.Where(x => !assignedCount.ContainsKey(x) || (assignedCount.ContainsKey(x) && assignedCount[x] < index + 1));

                            // Randomly select a player from the unassigned
                            var unassigned = randomizedPlayers.Where(x => !playersAlreadyAssigned.Contains(x)
                                                                        && (underAssignedPlayers.Count() == 0 || underAssignedPlayers.Contains(x)));
                            var random = new Random();
                            player = unassigned.ElementAt(random.Next(0, unassigned.Count()));
                        }

                        if (assignedCount.ContainsKey(player))
                        {
                            assignedCount[player]++;
                        }
                        else
                        {
                            assignedCount.Add(player, 1);
                        }

                        substitutions.Add(new Substitution
                        {
                            Id = Guid.NewGuid(),
                            Player = player,
                            Position = position,
                            Rotation = rotation,
                        });
                        numberAssigned++;
                    }
                }

                // Populate the unlimited position
                var unassignedPlayers = players.Where(x => !substitutions.Exists(y => y.Rotation == rotation && y.Player == x));
                var unlimitedPosition = positions.FirstOrDefault(x => x.NumberAllowed == null);
                if (unlimitedPosition != null)
                {
                    foreach (var player in unassignedPlayers)
                    {
                        substitutions.Add(new Substitution
                        {
                            Id = Guid.NewGuid(),
                            Player = player,
                            Position = unlimitedPosition,
                            Rotation = rotation,
                        });
                    }
                }
            }

            return substitutions;
        }

        public IEnumerable<Game> GetGamesByTeam(Guid teamId)
        {
            var team = _teamService.GetTeam("asdf");
            return new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid(),
                    TeamId = teamId,
                    Team = team,
                    AttendingPlayers = team.Players,
                    GameStart = DateTime.Now
                }
            };
        }
    }
}
