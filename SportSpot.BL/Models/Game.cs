using System.Text.Json.Serialization;

namespace SportSpot.BL.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        [JsonIgnore]
        public Team Team { get; set; }
        public DateTime GameStart { get; set; }
        public IEnumerable<Guid> AttendingPlayerIds { get; set; }
        [JsonIgnore]
        public IEnumerable<Player> AttendingPlayers { get; set; }
        public IEnumerable<Guid> SubstitutionIds { get; set; }
        [JsonIgnore]
        public IEnumerable<Substitution> Substitutions { get; set; }
    }
}
