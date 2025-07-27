using NotificationService.Application.Models.Requests;
namespace NotificationService.Application.Contracts;

public interface IEmailService
{
    Task<bool> SendSuccessOrderEmailAsync(OrderSuccessEmailRequestModel requestModel);
    Task<bool> SendEmailAsync(EmailDto requestModel);
}