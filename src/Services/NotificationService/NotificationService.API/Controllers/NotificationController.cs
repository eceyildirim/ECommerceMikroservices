using System.Net;
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
    public async Task<IActionResult> SendEmail([FromBody] OrderEmailRequestModel emailRequestModel)
    {
        return Ok(await _emailService.SendEmailAsync(emailRequestModel));
    }

    [HttpPost, Route("send-sms")]
    public async Task<IActionResult> SendSMS([FromBody] OrderSMSRequestModel smsRequestModel)
    {
        var result = await _smsService.SendSMSAsync(smsRequestModel);

        if (!result)
            return BadRequest();

        return Ok();
    }

}
