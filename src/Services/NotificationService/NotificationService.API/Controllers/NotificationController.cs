using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Contracts;
using NotificationService.Application.Models.Requests;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : BaseController<NotificationController>
{
    private readonly ISMSService _smsService;
    private readonly IEmailService _emailService;

    public NotificationController(ISMSService smsService, IEmailService emailService)
    {
        _emailService = emailService;
        _smsService = smsService;
    }

    [HttpPost, Route("send-email")]
    public async Task<IActionResult> SendEmail([FromBody] OrderSuccessEmailRequestModel emailRequestModel)
    {
        var response = await _emailService.SendSuccessOrderEmailAsync(emailRequestModel);

        return Ok();

    }

    [HttpPost, Route("send-sms")]
    public async Task<IActionResult> SendSMS([FromBody] OrderSuccessSMSRequestModel smsRequestModel)
    {
        var response = await _smsService.SendSuccessOrderSMSAsync(smsRequestModel);

        return Ok();
    }

}
