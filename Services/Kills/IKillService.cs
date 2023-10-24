using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Kills
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Kill entities.
    /// </summary>
    public interface IKillService : ICrudService<Kill, int>
    {
        /// <summary>
        /// Associates a location with a kill.
        /// </summary>
        /// <param name="killId">The ID of the kill to associate with a location.</param>
        /// <param name="locationId">The ID of the location to associate with the kill.</param>
        Task AddLocationToKillAsync(int killId, int locationId);
    }
}


