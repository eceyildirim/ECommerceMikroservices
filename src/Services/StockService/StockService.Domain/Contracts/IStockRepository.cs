using StockService.Domain.Entities;
namespace StockService.Domain.Contracts;

public interface IStockRepository : IRepository<Stock>
{
    Task<Stock> GetStockByProductId(Guid id);
}