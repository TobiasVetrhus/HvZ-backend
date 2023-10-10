using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfKill { get; set; }
    }
}
