using NotificationService.Application.Contracts;
using NotificationService.Application.Models.Requests;
using Twilio;
using Microsoft.Extensions.Configuration;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NotificationService.Application.Services;

public class SMSService : ISMSService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _from;

    public SMSService(IConfiguration configuration)
    {
        _accountSid = configuration["Twilio:AccountSid"]!;
        _authToken = configuration["Twilio:AuthToken"]!;
        _from = configuration["Twilio:FromPhoneNumber"]!;

    }

    public async Task<bool> SendSuccessOrderSMSAsync(OrderSuccessSMSRequestModel requestModel)
    {
        var message = $"Sayın {requestModel.Order.CustomerNameSurname}, siparişiniz başarıyla onaylandı. Teşekkür ederiz.";

        SMSDto smsDto = new SMSDto
        {
            Message = message,
            PhoneNumber = requestModel.PhoneNumber
        };

        var result = await SendSMSAsync(smsDto);

        return result;
    }
    public async Task<bool> SendSMSAsync(SMSDto requestModel)
    {
        try
        {
            TwilioClient.Init(_accountSid, _authToken);

            var msg = await MessageResource.CreateAsync(
                to: new PhoneNumber(requestModel.PhoneNumber),
                from: new PhoneNumber(_from),
                body: requestModel.Message
            );

            // msg.ErrorCode null ise genelde başarılıdır.
            return msg.ErrorCode == null;
        }
        catch (Exception)
        {
            // TODO: logla
            return false;
        }
    }

}