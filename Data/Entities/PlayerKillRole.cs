namespace HvZ_backend.Data.Entities
{
    public enum KillRoleType
    {
        Victim,
        Killer
    }

    public class PlayerKillRole
    {
        public int Id { get; set; }
        public int PlayerId { get; set; } // References the player
        public int KillId { get; set; }   // References the kill
        public KillRoleType RoleType { get; set; } // Role of the player in the kill (e.g., "Victim" or "Killer")

        // Navigation properties
        public Player Player { get; set; }
        public Kill Kill { get; set; }
    }
}
