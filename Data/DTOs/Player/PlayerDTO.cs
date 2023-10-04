using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Player
{
    public class PlayerDTO
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Username { get; set; }
        public bool Zombie { get; set; }

        [StringLength(8)]
        public string BiteCode { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int SquadId { get; set; }
    }
}