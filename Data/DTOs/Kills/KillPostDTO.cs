using System;
using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillPostDTO
    {
        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime TimeOfKill { get; set; }
    }
}