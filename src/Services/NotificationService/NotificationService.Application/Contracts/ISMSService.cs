using NotificationService.Application.Models.Requests;
namespace NotificationService.Application.Contracts;

public interface ISMSService
{
    Task<bool> SendSuccessOrderSMSAsync(OrderSuccessSMSRequestModel requestModel);
    Task<bool> SendSMSAsync(SMSDto requestModel);
}