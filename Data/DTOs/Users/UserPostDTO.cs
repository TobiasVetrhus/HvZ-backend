using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Users
{
    public class UserPostDTO
    {

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$")]
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
