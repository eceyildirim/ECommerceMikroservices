using NotificationService.Application.Contracts;
using NotificationService.Common.Models.Requests;
using MailKit.Net.Smtp;
using System.Text;
using NotificationService.Common.Exceptions;
using MimeKit;
using NotificationService.Domain.Contracts;
using NotificationService.Domain.Entities;
using NotificationService.Common.Models;
using Microsoft.Extensions.Configuration;
using NotificationService.Common.Enums;
using Microsoft.Extensions.Options;
using AutoMapper;
namespace NotificationService.Application.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    private readonly IRepository<NotificationLog> _notificationLogRepository;

    public EmailService(IConfiguration configuration, IRepository<NotificationLog> notificationLogRepository, IMapper mapper, IOptions<AppSettings> appSettings)
    {
        _configuration = configuration;
        _notificationLogRepository = notificationLogRepository;
        _mapper = mapper;
        _appSettings = appSettings.Value;

    }
    private async Task SendCancelledOrderMail(OrderEmailRequestModel requestModel)
    {
        string template = @"<!DOCTYPE html>
                            <html lang='tr'>
                                <head>
                                    <meta charset='UTF-8'>
                                    <title>Siparişiniz İptal Edildi</title>
                                    <style>
                                        body {
                                        font-family: Arial, sans-serif;
                                        background-color: #f2f2f2;
                                        color: #333;
                                        line-height: 1.6;
                                        padding: 20px;
                                        }

                                        .container {
                                        max-width: 600px;
                                        margin: auto;
                                        background-color: #ffffff;
                                        padding: 30px;
                                        border-radius: 8px;
                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.05);
                                        }

                                        .header {
                                        font-size: 22px;
                                        font-weight: bold;
                                        margin-bottom: 20px;
                                        color: #d35400;
                                        }

                                        .footer {
                                        margin-top: 30px;
                                        font-size: 13px;
                                        color: #999;
                                        }

                                        .btn {
                                        display: inline-block;
                                        margin-top: 20px;
                                        padding: 10px 20px;
                                        background-color: #2980b9;
                                        color: #fff;
                                        text-decoration: none;
                                        border-radius: 5px;
                                        }

                                        .btn:hover {
                                        background-color: #1c5980;
                                        }
                                    </style>
                                    </head>

                                    <body>
                                        <div class='container'>
                                            <div class='header'>Siparişiniz İptal Edildi</div>

                                                <p>Sayın<strong>{{CustomerNameSurname
                                            }}</ strong >,</ p >

                                                <p>{ { OrderId} }
                                            numaralı siparişiniz, talebiniz doğrultusunda başarıyla iptal edilmiştir.</p>

                                                <p>Ödemeniz varsa iade süreci en kısa sürede başlatılacaktır. İade durumu ve diğer detaylar için hesabınızı kontrol
                                                edebilirsiniz.</p>

                                        <p class= 'footer'>
                                        Herhangi bir sorunuz varsa bizimle <a href='mailto:destek@example.com'>iletişime geçebilirsiniz</a>.<br>
                                        © 2025 ECommerceMicroservices | Müşteri Hizmetleri
                                        </p>
                                    </div>
                                    </body>

                                    </html>";

        template = template.Replace("{{CustomerNameSurname}}", requestModel.Order.Customer.Name + " " + requestModel.Order.Customer.Surname)
            .Replace("{{OrderId}}", requestModel.Order.Id.ToString());

        EmailDto emailDto = new EmailDto
        {
            To = requestModel.Order.Customer.Email,
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

        string template = @"
                <!DOCTYPE html>
                <html lang='tr'>
                <head>
                    <meta charset='UTF-8'>
                    <title>Siparişiniz İptal Edildi</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            color: #333;
                            line-height: 1.6;
                            padding: 20px;
                        }
                        .container {
                            max-width: 600px;
                            margin: auto;
                            background-color: #ffffff;
                            padding: 30px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.05);
                        }
                        .header {
                            font-size: 20px;
                            font-weight: bold;
                            margin-bottom: 20px;
                            color: #c0392b;
                        }
                        .footer {
                            margin-top: 30px;
                            font-size: 13px;
                            color: #999;
                        }
                        .btn {
                            display: inline-block;
                            margin-top: 20px;
                            padding: 10px 20px;
                            background-color: #e74c3c;
                            color: #fff;
                            text-decoration: none;
                            border-radius: 5px;
                        }
                        .btn:hover {
                            background-color: #c0392b;
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>Siparişiniz İptal Edildi</div>

                        <p>Sayın <strong>{{CustomerNameSurname}}</strong>,</p>

                        <p>Vermiş olduğunuz <strong>{{OrderId}}</strong> numaralı siparişiniz, stok durumları nedeniyle <strong>iptal edilmiştir</strong>.</p>

                        <p>Dilerseniz farklı ürünleri inceleyebilir ya da benzer ürünleri tekrar sipariş edebilirsiniz.</p>

                        <p class='footer'>
                            Bu e-posta otomatik olarak gönderilmiştir. Lütfen yanıtlamayınız.<br>
                            © {{Year}} ECommerceMicroservices | Müşteri Hizmetleri
                        </p>
                    </div>
                </body>
                </html>
                ";

        template = template.Replace("{{CustomerNameSurname}}", requestModel.Order.Customer.Name + " " + requestModel.Order.Customer.Surname)
                    .Replace("{{OrderId}}", requestModel.Order.Id.ToString());

        EmailDto emailDto = new EmailDto
        {
            To = requestModel.Order.Customer.Email,
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
        catch (Exception exp)
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

            throw new EmailNotificationSendFailedException();
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
        string template = @"<!DOCTYPE html>
                                <html lang='tr'>

                                < head >
                                    < meta charset = 'UTF-8' >
                                    < title > Sipariş Onayı </ title >
                                    < style >
                                        body {
                                            font - family: 'Segoe UI', sans - serif;
                                            background - color: #f7f7f7;
                                            padding: 20px;
                                        color: #333;
                                        }

                                    .container {
                                        background - color: #ffffff;
                                        padding: 30px;
                                        border - radius: 8px;
                                        box - shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                        max - width: 600px;
                                    margin: auto;
                                    }

                                    .header {
                                        border - bottom: 2px solid #eeeeee;
                                        margin - bottom: 20px;
                                    }

                                    .header h2 {
                                    margin: 0;
                                    color: #2c3e50;
                                    }

                                    .order - info,
                                    .product - list,
                                    .total {
                                        margin - top: 20px;
                                    }

                                    table {
                                    width: 100 %;
                                        border - collapse: collapse;
                                        margin - top: 10px;
                                    }

                                    th,
                                    td {
                                    padding: 10px;
                                        border - bottom: 1px solid #eeeeee;
                                        text - align: left;
                                    }

                                    .total {
                                        font - weight: bold;
                                        text - align: right;
                                    }

                                    .footer {
                                        margin - top: 30px;
                                        font - size: 14px;
                                    color: #888;
                                        text - align: center;
                                    }
                                </ style >
                            </ head >
                            <body>
                                <div class='container'>
                                    <div class='header'>
                                        <h2>Siparişiniz Alındı, Teşekkürler {{CustomerName
                            }}!</ h2 >
                                    </ div >

                                    < div class= 'order-info' >
                                        < p >< strong > Sipariş Tarihi:</ strong > { { OrderDate} }</ p >
                                        < p >< strong > Sipariş No: </ strong > { { OrderNo} }</ p >
                                    </ div >

                                    < div class= 'product-list' >
                                        < h4 > Sipariş Detayları:</ h4 >
                                        <table>
                                            < thead >
                                                < tr >
                                                    < th > Ürün </ th >
                                                    < th > Adet </ th >
                                                    < th > Birim Fiyat </ th >
                                                    < th > Toplam </ th >
                                                </ tr >
                                            </ thead >
                                            < tbody >
                                                { { ProductRows} }
                                            </ tbody >
                                        </ table >
                                    </ div >

                                    < div class= 'total' >
                                        < p > Genel Toplam: { { TotalPrice} }</ p >
                                    </ div >

                                    < div class= 'footer' >
                                        < p > Ecommercemicroservices'i tercih ettiğiniz için teşekkür ederiz.</p>
                                    </ div >
                                </ div >
                            </ body >

                            </ html > ";

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

        template = template.Replace("{{CustomerNameSurname}}", requestModel.Order.Customer.Name + " " + requestModel.Order.Customer.Surname)
                           .Replace("{{OrderDate}}", requestModel.Order.OrderDate.ToString("dd MMMM yyyy"))
                           .Replace("{{ProductRows}}", productRows.ToString())
                           .Replace("{{TotalAmount}}", requestModel.Order.TotalAmount.ToString("N2"));

        EmailDto emailDto = new EmailDto
        {
            To = requestModel.Order.Customer.Email,
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

            throw new EmailNotificationSendFailedException();
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

        switch (requestModel.Order.Status)
        {
            case OrderStatus.OperationalCancelled:
                await SendOperationCancelledOrderMail(requestModel);
                break;
            case OrderStatus.Completed:
                await SendSuccessedOrderMail(requestModel);
                break;
            case OrderStatus.Cancelled:
                await SendCancelledOrderMail(requestModel);
                break;
        }

        return true;
    }

    public async Task SendEmailAsync(EmailDto requestModel)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_appSettings.EmailSettings.From));
        email.To.Add(MailboxAddress.Parse(requestModel.To));
        email.Subject = requestModel.Subject;
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = requestModel.Body
        };

        email.Body = bodyBuilder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync(_appSettings.EmailSettings.SmtpServer,
            587,
            false);

        await smtp.AuthenticateAsync(_appSettings.EmailSettings.UserName,
            _appSettings.EmailSettings.Password);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);

    }

}