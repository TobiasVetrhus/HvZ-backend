namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillDTO
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int? LocationId { get; set; }
        public int? XCoordinate { get; set; }
        public int? YCoordinate { get; set; }
        public DateTime TimeOfKill { get; set; }
    }
}
