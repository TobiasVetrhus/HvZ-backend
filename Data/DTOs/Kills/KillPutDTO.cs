using System;
using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Kills
{
    public class KillPutDTO
    {
        [StringLength(255)]
        public string Description { get; set; }

        public DateTime TimeOfKill { get; set; }
    }
}