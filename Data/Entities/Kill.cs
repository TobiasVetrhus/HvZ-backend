namespace HvZ_backend.Data.Entities
{
    public class Kill
    {
        public int Id { get; set; }
        public DateTime TimeOfKill { get; set; }
        public int? PlayerId { get; set; }
        public int? LocationId { get; set; }
        public Player Player { get; set; }
        public Location Location { get; set; }
    }
}
