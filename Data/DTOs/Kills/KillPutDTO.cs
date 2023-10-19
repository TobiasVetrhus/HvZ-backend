namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillPutDTO
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public DateTime TimeOfKill { get; set; }
    }
}