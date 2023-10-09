using HvZ_backend.Data.Entities;

namespace HvZ_backend.Data.DTOs.Conversations
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public string ConversationName { get; set; }
        public ChatType ChatType { get; set; }
        public int GameId { get; set; }
        public int[]? Messages { get; set; }
    }
}
