using NotificationService.WorkerService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using NotificationService.WorkerService.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
namespace NotificationService.WorkerService;

public class NotificationQueueConsumer : BackgroundService
{
    private readonly ILogger<NotificationQueueConsumer> _logger;
    private readonly AppSettings _appSettings;
    private readonly HttpClient _httpClient;

    public NotificationQueueConsumer(ILogger<NotificationQueueConsumer> logger, IOptions<AppSettings> options, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
        _appSettings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var factory = new ConnectionFactory()
        {
            HostName = _appSettings.QueueConfiguration.HostName,
            Port = _appSettings.QueueConfiguration.Port,
            UserName = _appSettings.QueueConfiguration.UserName,
            Password = _appSettings.QueueConfiguration.Password
        };

        var _connection = await factory.CreateConnectionAsync().ConfigureAwait(false);
        var _channel = await _connection.CreateChannelAsync().ConfigureAwait(false);

        _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
        if (_channel == null)
            throw new InvalidOperationException("RabbitMQ channel is not created. Call ConnectAsync first.");

        await _channel.ExchangeDeclareAsync(_appSettings.QueueConfiguration.ExchangeName, ExchangeType.Direct);
        await _channel.QueueDeclareAsync(_appSettings.QueueConfiguration.NotificationQueueName, true, false, false, null);
        await _channel.QueueBindAsync(_appSettings.QueueConfiguration.NotificationQueueName, _appSettings.QueueConfiguration.ExchangeName, _appSettings.QueueConfiguration.NotificationRoutingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[NotificationConsumer] Mesaj alındı: {message}");

                var notificationMessage = JsonConvert.DeserializeObject<NotificationMessage>(message);

                // Örnek: Mesajı işleme simülasyonu
                if (notificationMessage != null)
                {
                    Console.WriteLine($"OrderId: {notificationMessage.Order.Id}, Items count: {notificationMessage.Order.Items.Count}");

                    if (notificationMessage.ChannelTypes.Contains(ChannelType.Email))
                    {
                        _httpClient.BaseAddress = new Uri(_appSettings.NotificationServiceAPIUrl);
                        var response = await _httpClient.PutAsJsonAsync("api/notification/send-email", message);
                    }

                    if (notificationMessage.ChannelTypes.Contains(ChannelType.SMS))
                    {
                        _httpClient.BaseAddress = new Uri(_appSettings.NotificationServiceAPIUrl);
                        var response = await _httpClient.PutAsJsonAsync("api/notification/send-sms", message);
                    }
                }

                // Başarılı işlendiyse RabbitMQ'ya ack gönder
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StockConsumer] HATA: {ex.Message}");

                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
            }

            await Task.Yield();
        };


        await _channel.BasicConsumeAsync(
                queue: _appSettings.QueueConfiguration.NotificationQueueName,
                autoAck: false,
                consumer: consumer);


        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
