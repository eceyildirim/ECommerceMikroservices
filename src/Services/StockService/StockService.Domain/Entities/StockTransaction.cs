using StockService.Domain.Entities;
using StockService.Domain.Enums;
namespace StockService.Domain.Entities;

public class StockTransaction
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid? OrderId { get; set; }
    public int QuantityChanged { get; set; } // +/-
    public StockTransactionType Type { get; set; } // Reserve, Commit, Release, ManualAdjust...
    public DateTime OccurredAt { get; set; }
    public string? Reason { get; set; }
    public Product Product { get; set; } = null!;
}