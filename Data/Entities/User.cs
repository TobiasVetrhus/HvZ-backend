using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "First Name should be up to 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name should be up to 50 characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email should be up to 100 characters.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        [MaxLength(15, ErrorMessage = "Phone number should be up to 15 characters.")]
        public string Phone { get; set; }

        public ICollection<Player>? Players { get; set; }
    }
}

