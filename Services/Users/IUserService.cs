using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Users
{
    public interface IUserService : ICrudService<User, int>
    {
        Task UpdatePlayersAsync(int userId, int[] playerIds);
    }
}
