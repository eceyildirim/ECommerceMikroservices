namespace StockService.Domain.Entities;

public class Stock : BaseEntity
{
    public Guid ProductId { get; set; }
    public int QuantityAvailable { get; set; }
    public Product Product { get; set; }
    public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
}