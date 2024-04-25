namespace SportSpot.BL.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Number of players allowed in the position. Null for any number. 0 for none.
        /// </summary>
        public int? NumberAllowed { get; set; }
    }
}
