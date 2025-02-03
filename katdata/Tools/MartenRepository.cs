using Marten;

namespace katdata.Tools;

public class MartenRepository<T, TId>(IDocumentSession session) : Repository<T, TId> where T : class
{
    private readonly IDocumentSession _session = session;

    public async Task<T?> GetByIdAsync(TId id)
    {
        return await _session.LoadAsync<T>(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _session.Query<T>().ToListAsync();
    }

    public async Task SaveAsync(T entity)
    {
        _session.Store(entity);
        await _session.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        _session.Delete<T>(id);
        await _session.SaveChangesAsync();
    }
}
