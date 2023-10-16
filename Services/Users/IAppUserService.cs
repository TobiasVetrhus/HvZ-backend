using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Users
{
    public interface IAppUserService : ICrudService<AppUser, Guid>
    {
        Task AddPlayerAsync(Guid userId, int playerId);
        //Task RemovePlayerAsync(int userId, int playerId);
        Task UpdatePlayersAsync(Guid userId, int[] playerIds);
        Task<AppUser> GetUserIfExists(Guid id);
    }
}
