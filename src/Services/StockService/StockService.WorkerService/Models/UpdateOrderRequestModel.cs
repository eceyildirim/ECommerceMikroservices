using StockService.WorkerService.Enums;
namespace StockService.WorkerService.Models;

public class UpdateOrderRequestModel
{
    public Guid OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}