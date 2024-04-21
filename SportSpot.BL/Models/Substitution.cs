using System.Text.Json.Serialization;

namespace SportSpot.BL.Models
{
    public class Substitution
    {
        public Guid Id { get; set; }
        public Position Position { get; set; }
        public Player Player { get; set; }
        public Rotation Rotation { get; set; }
    }
}
