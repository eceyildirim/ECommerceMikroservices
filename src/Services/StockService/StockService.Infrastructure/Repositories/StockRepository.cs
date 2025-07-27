using StockService.Domain.Contracts;
using StockService.Domain.Entities;
using StockService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace StockService.Infrastructure.Repositories;

public class StockRepository : Repository<Stock>, IStockRepository
{
    public StockRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Stock> GetStockByProductId(Guid id)
    {
        return await _context.Set<Stock>()
            .Include(o => o.Product)
            .FirstOrDefaultAsync(o => o.ProductId == id);
    }
}