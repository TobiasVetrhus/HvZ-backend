using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Squads
{
    public interface ISquadService : ICrudService<Squad, int>
    {
        Task UpdatePlayersAsync(int squadId, int[] playerIds);
        Task<ICollection<Squad>> GetSquadsBySizeAsync(int minSize, int maxSize);
        Task<ICollection<Player>> GetSquadPlayersAsync(int squadId);
    }
}
