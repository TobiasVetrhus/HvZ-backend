using HvZ_backend.Data.Entities;

namespace HvZ_backend.Services.Locations
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on Location entities.
    /// </summary>
    public interface ILocationService : ICrudService<Location, int>
    {
    }
}
