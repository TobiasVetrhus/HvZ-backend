using HvZ_backend.Data.Entities;

namespace HvZ_backend.Data.DTOs.PlayerKillRoles
{
    public class PlayerKillRolePutDTO
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int KillId { get; set; }
        public KillRoleType RoleType { get; set; }
    }
}
