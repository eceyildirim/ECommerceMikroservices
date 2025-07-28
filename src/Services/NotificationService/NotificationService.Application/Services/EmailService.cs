using NotificationService.Application.Contracts;
using NotificationService.Application.Models.Requests;
using MailKit.Net.Smtp;
using System.Text;
using MimeKit;
using NotificationService.Domain.Contracts;
using NotificationService.Domain.Entities;
using NotificationService.Application.Models;
using Microsoft.Extensions.Configuration;
using NotificationService.Application.Enums;
using AutoMapper;
namespace NotificationService.Application.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IRepository<NotificationLog> _notificationLogRepository;

    public EmailService(IConfiguration configuration, IRepository<NotificationLog> notificationLogRepository, IMapper mapper)
    {
        _configuration = configuration;
        _notificationLogRepository = notificationLogRepository;
        _mapper = mapper;
    }

    private async Task SendCancelledOrderMail(OrderEmailRequestModel requestModel)
    {
        string template = await File.ReadAllTextAsync("Templates/OrderOperationCancelled.html");

        template = template.Replace("{{CustomerNameSurname}}", requestModel.Order.CustomerNameSurname)
                    .Replace("{{OrderId}}", requestModel.Order.Id);

        EmailDto emailDto = new EmailDto
        {
            To = requestModel.To,
            Subject = "Sipariş İptali",
            Body = template
        };

        NotificationLogDto notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = emailDto.To,
            ChannelType = ChannelType.Email,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Response,
            JsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel)
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

        try
        {
            await SendEmailAsync(emailDto);

        }
        catch
        {
            notificationLogDto = new NotificationLogDto
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Receiver = emailDto.To,
                ChannelType = ChannelType.Email,
                TransactionId = requestModel.Order.Id,
                TransactionType = TransactionType.Order,
                LogDate = DateTime.UtcNow,
                LogType = LogType.Response,
                JsonValue = "Mail gönderimi başarısız"
            };
            await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

            throw;
        }

        notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = emailDto.To,
            ChannelType = ChannelType.Email,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Response,
            JsonValue = "Mail gönderimi başarılı"
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));
    }

    private async Task SendOperationCancelledOrderMail(OrderEmailRequestModel requestModel)
    {
        string template = await File.ReadAllTextAsync("Templates/OrderOperationCancelled.html");

        template = template.Replace("{{CustomerNameSurname}}", requestModel.Order.CustomerNameSurname)
                    .Replace("{{OrderId}}", requestModel.Order.Id);

        EmailDto emailDto = new EmailDto
        {
            To = requestModel.To,
            Subject = "Siparişiniz İptali",
            Body = template
        };

        NotificationLogDto notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = emailDto.To,
            ChannelType = ChannelType.Email,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Response,
            JsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel)
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

        try
        {
            await SendEmailAsync(emailDto);

        }
        catch
        {
            notificationLogDto = new NotificationLogDto
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Receiver = emailDto.To,
                ChannelType = ChannelType.Email,
                TransactionId = requestModel.Order.Id,
                TransactionType = TransactionType.Order,
                LogDate = DateTime.UtcNow,
                LogType = LogType.Response,
                JsonValue = "Mail gönderimi başarısız"
            };
            await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

            throw;
        }

        notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = emailDto.To,
            ChannelType = ChannelType.Email,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Response,
            JsonValue = "Mail gönderimi başarılı"
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));
    }

    private async Task SendSuccessedOrderMail(OrderEmailRequestModel requestModel)
    {
        string template = await File.ReadAllTextAsync("Templates/OrderSuccess.html");

        var productRows = new StringBuilder();
        foreach (var item in requestModel.Order.Items)
        {
            var totalPrice = item.Quantity * item.UnitPrice;
            productRows.AppendLine($@"
            <tr>
                <td>{item.ProductName}</td>
                <td>{item.Quantity}</td>
                <td>{item.UnitPrice:C}</td>
                <td>{totalPrice:C}</td>
            </tr>
        ");
        }

        template = template.Replace("{{CustomerNameSurname}}", requestModel.Order.CustomerNameSurname)
                           .Replace("{{OrderDate}}", requestModel.Order.OrderDate.ToString("dd MMMM yyyy"))
                           .Replace("{{ProductRows}}", productRows.ToString())
                           .Replace("{{TotalAmount}}", requestModel.Order.TotalAmount.ToString("N2"));

        EmailDto emailDto = new EmailDto
        {
            To = requestModel.To,
            Subject = "Siparişiniz Alındı.",
            Body = template
        };

        NotificationLogDto notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = emailDto.To,
            ChannelType = ChannelType.Email,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Response,
            JsonValue = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel)
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

        try
        {
            await SendEmailAsync(emailDto);

        }
        catch
        {
            notificationLogDto = new NotificationLogDto
            {
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Receiver = emailDto.To,
                ChannelType = ChannelType.Email,
                TransactionId = requestModel.Order.Id,
                TransactionType = TransactionType.Order,
                LogDate = DateTime.UtcNow,
                LogType = LogType.Response,
                JsonValue = "Mail gönderimi başarısız"
            };
            await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));

            throw;
        }

        notificationLogDto = new NotificationLogDto
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Receiver = emailDto.To,
            ChannelType = ChannelType.Email,
            TransactionId = requestModel.Order.Id,
            TransactionType = TransactionType.Order,
            LogDate = DateTime.UtcNow,
            LogType = LogType.Response,
            JsonValue = "Mail gönderimi başarılı"
        };

        await _notificationLogRepository.AddAsync(_mapper.Map<NotificationLog>(notificationLogDto));
    }

    public async Task<bool> SendEmailAsync(OrderEmailRequestModel requestModel)
    {

        switch (requestModel.Order.OrderStatus)
        {
            case OrderStatus.OperationalCancelled:
                SendOperationCancelledOrderMail(requestModel);
                break;
            case OrderStatus.Completed:
                SendSuccessedOrderMail(requestModel);
                break;
            case OrderStatus.Cancelled:
                SendCancelledOrderMail(requestModel);
                break;
        }

        return true;
    }

    public async Task SendEmailAsync(EmailDto requestModel)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
        email.To.Add(MailboxAddress.Parse(requestModel.To));
        email.Subject = requestModel.Subject;
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = requestModel.Body
        };

        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
            int.Parse(_configuration["EmailSettings:Port"]),
            false);

        await smtp.AuthenticateAsync(_configuration["EmailSettings:Username"],
            _configuration["EmailSettings:Password"]);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

    }

}