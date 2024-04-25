using SportSpot.BL.Models;
using System.Text.Json;

namespace SportSpot.BL.Services
{
    public class FileDataService : IDataService
    {
        private readonly string DataLocation = "./StoredData";

        public async Task<Team?> GetTeam(Guid guid)
        {
            return (await LoadFromFile<Team>()).FirstOrDefault(x => x.Id == guid);
        }

        public async Task<Team> CreateTeam(Team team)
        {
            var teams = (await LoadFromFile<Team>()).ToList();
            team.Id = Guid.NewGuid();
            teams.Add(new Team
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description
            });

            bool success = await UpdateFile(teams);

            return await GetTeam(team.Id) ?? team;
        }

        public async Task<Team?> UpdateTeam(Team team)
        {
            var teams = (await LoadFromFile<Team>()).ToList();
            var existingTeam = teams.FirstOrDefault(x => x.Id == team.Id);
            if (existingTeam != null)
            {
                existingTeam.Name = team.Name;
                existingTeam.Description = team.Description;

                bool success = await UpdateFile(teams);
                if (!success)
                {
                    throw new Exception("Error updating team. Please try again!");
                }
            }

            return await GetTeam(team.Id);
        }

        public async Task<bool> DeleteTeam(Team team)
        {
            bool success = false;

            var teams = (await LoadFromFile<Team>()).ToList();
            var existingTeam = teams.FirstOrDefault(x => x.Id == team.Id);
            if (existingTeam != null)
            {
                // remove all games first (this also removes substitutions)
                var games = await GetGamesByTeam(team.Id);
                foreach (var game in games)
                {
                    await DeleteGame(game.Id, team.Id);
                }

                // remove all positions
                var positions = await GetPositionsByTeam(team.Id);
                foreach (var position in positions)
                {
                    await DeletePosition(position.Id, team.Id);
                }

                // remove all rotations
                var rotations = await GetRotationsByTeam(team.Id);
                foreach (var rotation in rotations)
                {
                    await DeleteRotation(rotation.Id, team.Id);
                }

                // remove all players
                var players = await GetPlayersByTeam(team.Id);
                foreach (var player in players)
                {
                    await DeletePlayer(player.Id, team.Id);
                }

                // remove team
                teams.Remove(existingTeam);

                success = await UpdateFile(teams);
            }

            return success;
        }

        public async Task<Game?> GetGame(Guid guid, Guid teamId)
        {
            return (await LoadFromFile<Game>()).FirstOrDefault(x => x.Id == guid && x.TeamId == teamId);
        }

        public async Task<IEnumerable<Game>> GetGamesByTeam(Guid teamId)
        {
            return (await LoadFromFile<Game>()).Where(x => x.TeamId == teamId);
        }

        public async Task<Game?> UpsertGame(Game game)
        {
            var games = (await LoadFromFile<Game>()).ToList();
            var existingGame = games.FirstOrDefault(x => x.Id == game.Id && x.TeamId == game.TeamId);
            if (existingGame != null)
            {
                existingGame.GameStart = game.GameStart;
                existingGame.AttendingPlayerIds = game.AttendingPlayerIds;
                existingGame.SubstitutionIds = game.SubstitutionIds;
            }
            else
            {
                game.Id = Guid.NewGuid();
                games.Add(new Game
                {
                    Id = game.Id,
                    TeamId = game.TeamId,
                    GameStart = game.GameStart,
                    AttendingPlayerIds = game.AttendingPlayerIds,
                    SubstitutionIds = game.SubstitutionIds
                });
            }

            bool success = await UpdateFile(games);

            return await GetGame(game.Id, game.TeamId);
        }

        public async Task<bool> DeleteGame(Guid gameId, Guid teamId)
        {
            bool success = false;

            var games = (await LoadFromFile<Game>()).ToList();
            var existingGame = games.FirstOrDefault(x => x.Id == gameId && x.TeamId == teamId);
            if (existingGame != null)
            {
                // Remove all substitutions
                await DeleteSubstitutionsByGame(existingGame.Id, teamId);
                
                games.Remove(existingGame);

                success = await UpdateFile(games);
            }

            return success;
        }

        public async Task<Player?> GetPlayer(Guid playerId, Guid teamId)
        {
            return (await LoadFromFile<Player>()).FirstOrDefault(x => x.Id == playerId && x.TeamId == teamId);
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeam(Guid teamId)
        {
            return (await LoadFromFile<Player>()).Where(x => x.TeamId == teamId);
        }

        public async Task<Player?> UpsertPlayer(Player player)
        {
            var players = (await LoadFromFile<Player>()).ToList();
            var existingPlayer = players.FirstOrDefault(x => x.Id == player.Id && x.TeamId == player.TeamId);
            if (existingPlayer != null)
            {
                existingPlayer.Name = player.Name;
            }
            else
            {
                player.Id = Guid.NewGuid();
                players.Add(new Player
                {
                    Id = player.Id,
                    TeamId = player.TeamId,
                    Name = player.Name
                });
            }

            bool success = await UpdateFile(players);

            return await GetPlayer(player.Id, player.TeamId);
        }

        public async Task<bool> DeletePlayer(Guid playerId, Guid teamId)
        {
            bool success = false;

            var players = (await LoadFromFile<Player>()).ToList();
            var existingPlayer = players.FirstOrDefault(x => x.Id == playerId && x.TeamId == teamId);
            if (existingPlayer != null)
            {
                // Remove all associated substitutions
                await DeleteSubstitutionsByPlayer(playerId, teamId);

                players.Remove(existingPlayer);

                success = await UpdateFile(players);
            }

            return success;
        }

        public async Task<Position?> GetPosition(Guid positionId, Guid teamId)
        {
            return (await LoadFromFile<Position>()).FirstOrDefault(x => x.Id == positionId && x.TeamId == teamId);
        }

        public async Task<IEnumerable<Position>> GetPositionsByTeam(Guid teamId)
        {
            return (await LoadFromFile<Position>()).Where(x => x.TeamId == teamId);
        }

        public async Task<Position?> UpsertPosition(Position position)
        {
            var positions = (await LoadFromFile<Position>()).ToList();
            var existingPosition = positions.FirstOrDefault(x => x.Id == position.Id && x.TeamId == position.TeamId);
            if (existingPosition != null)
            {
                existingPosition.Name = position.Name;
                existingPosition.NumberAllowed = position.NumberAllowed;
            }
            else
            {
                position.Id = Guid.NewGuid();
                positions.Add(new Position
                {
                    Id = position.Id,
                    TeamId = position.TeamId,
                    Name = position.Name,
                    NumberAllowed = position.NumberAllowed
                });
            }

            bool success = await UpdateFile(positions);

            return await GetPosition(position.Id, position.TeamId);
        }

        public async Task<bool> DeletePosition(Guid positionId, Guid teamId)
        {
            bool success = false;

            var positions = (await LoadFromFile<Position>()).ToList();
            var existingPosition = positions.FirstOrDefault(x => x.Id == positionId && x.TeamId == teamId);
            if (existingPosition != null)
            {
                await DeleteSubstitutionsByPosition(existingPosition.Id, existingPosition.TeamId);
                positions.Remove(existingPosition);

                success = await UpdateFile(positions);
            }

            return success;
        }

        public async Task<Rotation?> GetRotation(Guid rotationId, Guid teamId)
        {
            return (await LoadFromFile<Rotation>()).FirstOrDefault(x => x.Id == rotationId && x.TeamId == teamId);
        }

        public async Task<IEnumerable<Rotation>> GetRotationsByTeam(Guid teamId)
        {
            return (await LoadFromFile<Rotation>()).Where(x => x.TeamId == teamId);
        }

        public async Task<Rotation?> UpsertRotation(Rotation rotation)
        {
            var rotations = (await LoadFromFile<Rotation>()).ToList();
            var existingRotation = rotations.FirstOrDefault(x => x.Id == rotation.Id && x.TeamId == rotation.TeamId);
            if (existingRotation != null)
            {
                existingRotation.Name = rotation.Name;
            }
            else
            {
                rotation.Id = Guid.NewGuid();
                rotations.Add(new Rotation
                {
                    Id = rotation.Id,
                    TeamId = rotation.TeamId,
                    Name = rotation.Name
                });
            }

            bool success = await UpdateFile(rotations);

            return await GetRotation(rotation.Id, rotation.TeamId);
        }

        public async Task<bool> DeleteRotation(Guid rotationId, Guid teamId)
        {
            bool success = false;

            var rotations = (await LoadFromFile<Rotation>()).ToList();
            var existingRotation = rotations.FirstOrDefault(x => x.Id == rotationId && x.TeamId == teamId);
            if (existingRotation != null)
            {
                await DeleteSubstitutionsByRotation(existingRotation.Id, existingRotation.TeamId);
                rotations.Remove(existingRotation);

                success = await UpdateFile(rotations);
            }

            return success;
        }

        public async Task<Substitution?> GetSubstitution(Guid substitutionId, Guid gameId, Guid teamId)
        {
            Substitution? substitution = null;
            if ((await GetGame(gameId, teamId)) != null)
            {
                substitution = (await LoadFromFile<Substitution>()).FirstOrDefault(x => x.Id == substitutionId && x.GameId == gameId);
            }

            return substitution;
        }

        public async Task<IEnumerable<Substitution>> GetSubstitutionsByGame(Guid gameId, Guid teamId)
        {
            IEnumerable<Substitution> substitutions = Array.Empty<Substitution>();
            if ((await GetGame(gameId, teamId)) != null)
            {
                substitutions = (await LoadFromFile<Substitution>()).Where(x => x.GameId == gameId);
            }

            return substitutions;
        }

        public async Task<Substitution?> UpsertSubstitution(Substitution substitution, Guid teamId)
        {
            if ((await GetGame(substitution.GameId, teamId)) != null)
            {
                var substitutions = (await LoadFromFile<Substitution>()).ToList();
                var existingSubstitution = substitutions.FirstOrDefault(x => x.Id == substitution.Id && x.GameId == substitution.GameId);
                if (existingSubstitution != null)
                {
                    existingSubstitution.Player = substitution.Player;
                    existingSubstitution.Rotation = substitution.Rotation;
                    existingSubstitution.Position = substitution.Position;
                }
                else
                {
                    substitution.Id = Guid.NewGuid();
                    substitutions.Add(new Substitution
                    {
                        Id = substitution.Id,
                        GameId = substitution.GameId,
                        Player = substitution.Player,
                        Rotation = substitution.Rotation,
                        Position = substitution.Position
                    });
                }

                bool success = await UpdateFile(substitutions);
            }

            return await GetSubstitution(substitution.Id, substitution.GameId, teamId);
        }

        public async Task<bool> DeleteSubstitution(Guid substitutionId, Guid gameId, Guid teamId)
        {
            bool success = false;

            if ((await GetGame(gameId, teamId)) != null)
            {
                var substitutions = (await LoadFromFile<Substitution>()).ToList();
                var existingSubstitution = substitutions.FirstOrDefault(x => x.Id == substitutionId && x.GameId == gameId);
                if (existingSubstitution != null)
                {
                    substitutions.Remove(existingSubstitution);

                    success = await UpdateFile(substitutions);
                }
            }

            return success;
        }

        public async Task DeleteSubstitutionsByGame(Guid gameId, Guid teamId)
        {
            var substitutions = await GetSubstitutionsByGame(gameId, teamId);
            foreach (var substitution in substitutions)
            {
                await DeleteSubstitution(substitution.Id, gameId, teamId);
            }
        }

        public async Task DeleteSubstitutionsByPlayer(Guid playerId, Guid teamId)
        {
            if ((await GetPlayer(playerId, teamId)) != null)
            {
                var substitutions = (await LoadFromFile<Substitution>()).Where(x => x.PlayerId == playerId);
                foreach (var substitution in substitutions)
                {
                    await DeleteSubstitution(substitution.Id, substitution.GameId, teamId);
                }
            }
        }

        public async Task DeleteSubstitutionsByPosition(Guid positionId, Guid teamId)
        {
            if ((await GetPosition(positionId, teamId)) != null)
            {
                var substitutions = (await LoadFromFile<Substitution>()).Where(x => x.PositionId == positionId);
                foreach (var substitution in substitutions)
                {
                    await DeleteSubstitution(substitution.Id, substitution.GameId, teamId);
                }
            }
        }

        public async Task DeleteSubstitutionsByRotation(Guid rotationId, Guid teamId)
        {
            if ((await GetRotation(rotationId, teamId)) != null)
            {
                var substitutions = (await LoadFromFile<Substitution>()).Where(x => x.RotationId == rotationId);
                foreach (var substitution in substitutions)
                {
                    await DeleteSubstitution(substitution.Id, substitution.GameId, teamId);
                }
            }
        }

        public async Task<IEnumerable<TeamUser>> GetTeamUsers()
        {
            return await LoadFromFile<TeamUser>();
        }

        public async Task<TeamUser?> GetTeamUser(Guid teamId)
        {
            return (await LoadFromFile<TeamUser>()).FirstOrDefault(x => x.TeamId == teamId);
        }

        public async Task<bool> UpsertTeamUser(TeamUser teamUser)
        {
            var success = false;
            var teamUsers = (await GetTeamUsers()).ToList();
            var existingTeamUser = teamUsers.FirstOrDefault(x => x.TeamId == teamUser.TeamId && x.Username == teamUser.Username);
            if (existingTeamUser != null)
            {
                existingTeamUser.PasswordHash = teamUser.PasswordHash;
            }
            else
            {
                teamUsers.Add(teamUser);
            }

            success = await UpdateFile(teamUsers);
            return success;
        }

        public async Task<bool> DeleteTeamUser(TeamUser teamUser)
        {
            var success = false;
            var teamUsers = (await GetTeamUsers()).ToList();
            var existingTeamUser = teamUsers.FirstOrDefault(x => x.TeamId == teamUser.TeamId);
            if (existingTeamUser != null)
            {
                teamUsers.Remove(existingTeamUser);
                success = await UpdateFile(teamUsers);
            }
            return success;
        }

        private async Task<IEnumerable<T>> LoadFromFile<T>()
        {
            string filePath = Path.Combine(DataLocation, $"{typeof(T).Name}.json");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            string fileJson = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<IEnumerable<T>>(fileJson);
        }

        private async Task<bool> UpdateFile<T>(IEnumerable<T> items)
        {
            bool success = false;

            try
            {
                File.WriteAllText(Path.Combine(DataLocation, $"{typeof(T).Name}.json"), JsonSerializer.Serialize(items));
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }
    }
}
