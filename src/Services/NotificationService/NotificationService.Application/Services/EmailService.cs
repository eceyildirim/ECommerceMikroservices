using NotificationService.Application.Contracts;
using NotificationService.Application.Models.Requests;
using MailKit.Net.Smtp;
using System.Text;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace NotificationService.Application.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendSuccessOrderEmailAsync(OrderSuccessEmailRequestModel requestModel)
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

        var sendEmailResult = await SendEmailAsync(emailDto);

        return sendEmailResult;
    }

    public async Task<bool> SendEmailAsync(EmailDto requestModel)
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

        try
        {
            await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                int.Parse(_configuration["EmailSettings:Port"]),
                false);

            await smtp.AuthenticateAsync(_configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            return true;
        }
        catch
        {
            return false;
        }
    }

}