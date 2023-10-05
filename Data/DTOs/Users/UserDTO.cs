using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int[] Players { get; set; }
    }
}
