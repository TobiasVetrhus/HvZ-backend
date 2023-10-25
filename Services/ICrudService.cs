namespace HvZ_backend.Services
{
    /// <summary>
    /// Interface for performing CRUD (Create, Read, Update, Delete) operations on entities of type TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to perform CRUD operations on.</typeparam>
    /// <typeparam name="ID">The type of the unique identifier for the entity.</typeparam>
    public interface ICrudService<TEntity, ID>
    {
        /// <summary>
        /// Adds a new entity to the data store.
        /// </summary>
        /// <param name="obj">The entity to be added.</param>
        /// <returns>The added entity with any modifications or generated values.</returns>
        Task<TEntity> AddAsync(TEntity obj);

        /// <summary>
        /// Retrieves all entities of type TEntity from the data store.
        /// </summary>
        /// <returns>A collection of all entities of type TEntity.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity of type TEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified unique identifier if found; otherwise, null.</returns>
        Task<TEntity> GetByIdAsync(ID id);

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="obj">The entity with updated information to be stored in the data store.</param>
        /// <returns>The updated entity with any modifications or generated values.</returns>
        Task<TEntity> UpdateAsync(TEntity obj);

        /// <summary>
        /// Deletes an entity from the data store by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        /// <returns>Task representing the asynchronous deletion operation.</returns>
        Task DeleteByIdAsync(ID id);
    }
}
