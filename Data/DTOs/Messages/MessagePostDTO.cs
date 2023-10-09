using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Messages
{
    public class MessagePostDTO
    {
        [StringLength(1000)]
        public string Content { get; set; }
        public DateTime Sent { get; set; }

        public int ConversationId { get; set; }
        public int PlayerId { get; set; }
    }
}
