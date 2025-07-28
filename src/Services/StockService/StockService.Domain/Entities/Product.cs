using StockService.Domain.Entities;
namespace StockService.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
}