using HvZ_backend.Data.Entities;

namespace HvZ_backend.Data.DTOs.PlayerKillRoles
{
    public class PlayerKillRolePostDTO
    {
        public int PlayerId { get; set; }
        public int KillId { get; set; }
        public KillRoleType RoleType { get; set; }
    }
}
