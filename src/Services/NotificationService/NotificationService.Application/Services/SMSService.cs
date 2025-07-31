using NotificationService.Application.Contracts;
using NotificationService.Common.Models.Requests;
using Twilio;
using Microsoft.Extensions.Configuration;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using AutoMapper;
using NotificationService.Domain.Contracts;
using NotificationService.Domain.Entities;
using NotificationService.Common.Enums;
using NotificationService.Common.Models;
using NotificationService.Common.Exceptions;

namespace NotificationService.Application.Services;

public class SMSService : ISMSService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _from;

    private readonly IMapper _mapper;
    private readonly IRepository<NotificationLog> _notificationLogRepository;

    public SMSService(IConfiguration configuration, IMapper mapper, IRepository<NotificationLog> notificationLogRepository)
    {
        _accountSid = configuration["Twilio:AccountSid"]!;
        _authToken = configuration["Twilio:AuthToken"]!;
        _from = configuration["Twilio:FromPhoneNumber"]!;
        _mapper = mapper;
        _notificationLogRepository = notificationLogRepository;
    }
    private async Task<bool> SendOrderSMS(OrderSMSRequestModel requestModel, string message)
    {
        SMSDto smsDto = new SMSDto
        {
            Message = message,
            PhoneNumber = requestModel.Order.Customer.PhoneNumber
        };

        NotificationLogDto notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = smsDto.PhoneNumber,
            ChannelType = ChannelType.SMS,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Request,
            JsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel)
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

        try
        {
            var result = await SendSMSAsync(smsDto);
            notificationLogDto = new NotificationLogDto
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Receiver = smsDto.PhoneNumber,
                ChannelType = ChannelType.Email,
                TransactionId = requestModel.Order.Id,
                TransactionType = TransactionType.Order,
                LogDate = DateTime.UtcNow,
                LogType = LogType.Response,
                JsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(result)
            };

            await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

            return result.ErrorCode == null;

        }
        catch (SMSNotificationSendFailedException)
        {
            notificationLogDto = new NotificationLogDto
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Receiver = smsDto.PhoneNumber,
                ChannelType = ChannelType.Email,
                TransactionId = requestModel.Order.Id,
                TransactionType = TransactionType.Order,
                LogDate = DateTime.UtcNow,
                LogType = LogType.Response,
                JsonValue = "SMS Gönderimi Sırasında Hata Oluştu"
            };

            await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

            return false;
        }
    }


    public async Task<bool> SendSMSAsync(OrderSMSRequestModel requestModel)
    {

        var message = "";

        switch (requestModel.Order.Status)
        {
            case OrderStatus.OperationalCancelled:
                message = $"Sayın {requestModel.Order.Customer.Name + " " + requestModel.Order.Customer.Surname}, {requestModel.Order.Id} numaralı siparişiniz operasyonel süreçlerden iptal edilmiştir. Teşekkür ederiz.";
                await SendOrderSMS(requestModel, message);
                break;
            case OrderStatus.Completed:
                message = $"Sayın {requestModel.Order.Customer.Name + " " + requestModel.Order.Customer.Surname}, {requestModel.Order.Id} siparişiniz başarıyla onaylandı. Teşekkür ederiz.";
                await SendOrderSMS(requestModel, message);
                break;
            case OrderStatus.Cancelled:
                message = $"Sayın {requestModel.Order.Customer.Name + " " + requestModel.Order.Customer.Surname}, {requestModel.Order.Id} siparişiniz talebiniz üzerine iptal edilmiştir. Teşekkür ederiz.";
                await SendOrderSMS(requestModel, message);
                break;
        }

        return true;
    }
    private async Task<MessageResource> SendSMSAsync(SMSDto requestModel)
    {
        try
        {
            TwilioClient.Init(_accountSid, _authToken);

            var msg = await MessageResource.CreateAsync(
                to: new PhoneNumber(requestModel.PhoneNumber),
                from: new PhoneNumber(_from),
                body: requestModel.Message
            );

            return msg;
        }
        catch (Exception exp)
        {
            // TODO: log yönetimi
            throw new SMSNotificationSendFailedException(exp.Message);
        }
    }

}