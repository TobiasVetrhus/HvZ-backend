namespace HvZ_backend.Data.DTOs.Messages
{
    public class MessagePostDTO
    {
        public string Content { get; set; }
        public DateTime Sent { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
    }
}
