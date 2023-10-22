using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Squads
{
    public interface ISquadService : ICrudService<Squad, int>
    {
        Task<ICollection<Squad>> GetSquadsByGameAsync(int gameId);
        Task UpdatePlayersAsync(int squadId, int[] playerIds);
        Task AddPlayerAsync(int squadId, int player);
        Task RemovePlayerAsync(int squadId, int player);
        Task AddGameToSquadAsync(int squadId, int gameId);
        Task<ICollection<Squad>> GetSquadsBySizeAsync(int minSize, int maxSize);
        Task<ICollection<Player>> GetSquadPlayersAsync(int squadId);
    }
}
