using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Guid ShippingAddressId { get; set; }
    public Customer? Customer { get; set; }
    public Address? ShippingAddress { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}