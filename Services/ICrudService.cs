namespace HvZ_backend.Services
{
    public interface ICrudService<TEntity, ID>
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(ID id);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task<TEntity> DeleteByIdAsync(ID id);
    }
}
