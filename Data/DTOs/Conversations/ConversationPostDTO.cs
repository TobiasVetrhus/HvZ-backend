using HvZ_backend.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.DTOs.Conversations
{
    public class ConversationPostDTO
    {
        [StringLength(50)]
        public string ConversationName { get; set; }
        public ChatType ChatType { get; set; }
        public int GameId { get; set; }
        public int[]? Messages { get; set; }
    }
}
