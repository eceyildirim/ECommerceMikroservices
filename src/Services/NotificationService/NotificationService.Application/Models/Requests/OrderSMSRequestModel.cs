namespace NotificationService.Application.Models.Requests;

public class OrderSMSRequestModel
{
    public string PhoneNumber { get; set; }
    public Order Order { get; set; }
}