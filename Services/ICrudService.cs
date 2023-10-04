namespace HvZ_backend.Services
{
    public interface ICrudService<TEntity, ID>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(ID id);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task<TEntity> DeleteByIdAsync(ID id);
    }
}
