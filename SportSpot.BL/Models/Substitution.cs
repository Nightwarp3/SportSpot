using System.Text.Json.Serialization;

namespace SportSpot.BL.Models
{
    public class Substitution
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public Guid RotationId { get; set; }
        public Rotation Rotation { get; set; }
    }
}
