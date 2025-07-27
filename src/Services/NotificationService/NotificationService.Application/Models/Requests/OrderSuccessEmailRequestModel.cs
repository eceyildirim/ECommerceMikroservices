namespace NotificationService.Application.Models.Requests;

public class OrderSuccessEmailRequestModel
{
    public string To { get; set; }
    public Order Order { get; set; }
}

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerNameSurname { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
