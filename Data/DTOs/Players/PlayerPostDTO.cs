using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Player
{
    public class PlayerPostDTO
    {
        [StringLength(100)]
        public string Username { get; set; }
        public int GameId { get; set; }
    }
}