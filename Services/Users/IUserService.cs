using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Users
{
    public interface IUserService : ICrudService<User, int>
    {
        Task AddPlayerAsync(int userId, int playerId);
        //Task RemovePlayerAsync(int userId, int playerId);
        Task UpdatePlayersAsync(int userId, int[] playerIds);
    }
}
