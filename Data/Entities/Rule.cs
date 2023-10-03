using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
