namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillPostDTO
    {
        public int? PlayerId { get; set; }
        public DateTime TimeOfKill { get; set; }
        public int? LocationId { get; set; }
    }
}