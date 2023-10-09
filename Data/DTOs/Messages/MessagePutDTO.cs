using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Messages
{
    public class MessagePutDTO
    {
        public int Id { get; set; }
        [StringLength(1000)]
        public string Content { get; set; }
        public DateTime Sent { get; set; }
    }
}
