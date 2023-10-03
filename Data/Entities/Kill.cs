using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ_backend.Data.Entities
{
    public class Kill
    {
        [Key]
        public int Id { get; set; } 

        [StringLength(255)] 
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "timestamp")]
        public DateTime TimeOfKill { get; set; } 

        [Required]
        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; } 

  
    }
}
