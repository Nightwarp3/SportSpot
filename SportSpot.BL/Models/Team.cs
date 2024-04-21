namespace SportSpot.BL.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public IEnumerable<Rotation> Rotations { get; set; }
    }
}
