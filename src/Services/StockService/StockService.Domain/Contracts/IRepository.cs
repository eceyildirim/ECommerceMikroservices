using System.Linq.Expressions;

namespace StockService.Domain.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = true, CancellationToken ct = default);
    Task<List<TEntity>> GetAllAsync(bool asNoTracking = true, CancellationToken ct = default);
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken ct = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default);
    IQueryable<TEntity> Query();
    Task AddAsync(TEntity entity, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}