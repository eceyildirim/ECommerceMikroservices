
namespace NotificationService.WorkerService.Models;

public class NotificationMessage
{
    //Email, SMS
    public List<ChannelType> ChannelTypes { get; set; }
    public Order Order { get; set; }

}

public enum ChannelType
{
    Email = 1,
    SMS = 2
}

public enum OrderStatus
{
    Pending = 0,        // Sipariş alındı, işlem bekleniyor
    Completed = 1,     // Sipariş hazırlanıyor
    Cancelled = 2,     // Müşteri siparişi iptal etti
    OperationalCancelled = 3 // Sipariş sistem tarafından stok yetersizliğinden iptal edildi
}

public class Customer
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}