namespace katdata.Tools;

public interface Repository<T, TId> where T : class
{
    public Task<T?> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveAsync(T entity);
    Task DeleteAsync(TId id);
}
