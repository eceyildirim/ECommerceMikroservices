namespace NotificationService.Application.Models.Requests;

public class OrderEmailRequestModel
{
    public string To { get; set; }
    public Order Order { get; set; }
}

public enum OrderStatus
{
    Pending = 0,        // Sipariş alındı, işlem bekleniyor
    Completed = 1,     // Sipariş hazırlanıyor
    Cancelled = 2,     // Müşteri siparişi iptal etti
    OperationalCancelled = 3 // Sipariş sistem tarafından stok yetersizliğinden iptal edildi
}

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerNameSurname { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
