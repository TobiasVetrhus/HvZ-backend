namespace HvZ_backend.Data.DTOs.Messages
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Sent { get; set; }

        public int ConversationId { get; set; }
        public int PlayerId { get; set; }
    }
}
