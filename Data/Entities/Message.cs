using System.ComponentModel.DataAnnotations.Schema;

namespace HvZ_backend.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
        public DateTime Sent { get; set; }
        public int? ConversationId { get; set; }
        public int? PlayerId { get; set; }

        //Navigation
        public Conversation Conversation { get; set; }
        public Player Player { get; set; }

    }
}
