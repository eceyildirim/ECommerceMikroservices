namespace StockService.Domain.Entities;

public class Stock : BaseEntity
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }

    public int QuantityAvailable { get; set; }
    public int ReservedQuantity { get; set; }
    public DateTime LastUpdated { get; set; }

    public byte[] RowVersion { get; set; } = default!;  // Optimistic concurrency

    public Product Product { get; set; } = null!;
}