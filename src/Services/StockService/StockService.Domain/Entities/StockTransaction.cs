using StockService.Domain.Entities;
using StockService.Domain.Enums;
namespace StockService.Domain.Entities;

public class StockTransaction
{
    public Guid Id { get; set; }
    public Guid? OrderId { get; set; }
    public int Quantity { get; set; }
    public StockTransactionType Type { get; set; } // Order, Cancel, Add
    public Guid StockId { get; set; }
    public Stock Stock { get; set; }
}