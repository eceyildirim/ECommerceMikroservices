using NotificationService.Common.Models.Requests;
namespace NotificationService.Application.Contracts;

public interface IEmailService
{
    Task<bool> SendEmailAsync(OrderEmailRequestModel requestModel);
}