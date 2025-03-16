using Microsoft.EntityFrameworkCore;

namespace katdata.Tools;

public class EfCoreRepository<T, TId> : Repository<T, TId> where T : class
{
    private readonly Context _context;

    public EfCoreRepository(Context context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task SaveAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TId id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}