using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Contract;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken ct = default)
    {
        var query = asNoTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, ct);
    }

    public async Task<List<TEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken ct = default)
    {
        return asNoTracking
            ? await _dbSet.AsNoTracking().ToListAsync(ct)
            : await _dbSet.ToListAsync(ct);
    }

    public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken ct = default)
    {
        return asNoTracking
            ? await _dbSet.AsNoTracking().Where(predicate).ToListAsync(ct)
            : await _dbSet.Where(predicate).ToListAsync(ct);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        => await _dbSet.AnyAsync(predicate, ct);

    public IQueryable<TEntity> Query() => _dbSet.AsQueryable();

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
        => await _dbSet.AddAsync(entity, ct);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        => await _dbSet.AddRangeAsync(entities, ct);

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public void Remove(TEntity entity) => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);        

}