using System.ComponentModel.DataAnnotations;

namespace HvZ_backend.Data.Entities
{
    public enum ChatType
    {
        Global,
        Squad,
        FactionZombies,
        FactionHumans
    }
    public class Conversation
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ConversationName { get; set; }
        public ChatType ChatType { get; set; }
        public int GameId { get; set; }

        //Navigation
        public Game game { get; set; }
        public ICollection<Player> Players { get; set; } //M-M

    }
}
