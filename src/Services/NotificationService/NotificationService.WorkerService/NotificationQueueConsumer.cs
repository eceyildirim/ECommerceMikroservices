using NotificationService.Common.Models;
using NotificationService.WorkerService.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
using NotificationService.Common.Enums;
using System.Net.Http.Json;
using NotificationService.Common.Models.Requests;
using Polly;
using Polly.Retry;
using System.Net.Http;
using NotificationService.WorkerService.Models;
namespace NotificationService.WorkerService;

public class NotificationQueueConsumer : BackgroundService
{
    private readonly ILogger<NotificationQueueConsumer> _logger;
    private readonly NotificationService.WorkerService.Models.AppSettings _appSettings;
    private readonly HttpClient _httpClient;
    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

    public NotificationQueueConsumer(ILogger<NotificationQueueConsumer> logger, IOptions<NotificationService.WorkerService.Models.AppSettings> options, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
        _appSettings = options.Value;

        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    _logger.LogWarning($"Retry {retryAttempt} after {timespan.TotalSeconds} seconds due to {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                });
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
                        var messageRequest = new OrderEmailRequestModel
                        {
                            Order = notificationMessage.Order
                        };

                        _httpClient.BaseAddress = new Uri(_appSettings.NotificationServiceAPIUrl);

                        var result = await _retryPolicy.ExecuteAsync(() =>
                            _httpClient.PutAsJsonAsync("api/notification/send-email", messageRequest));

                        result.EnsureSuccessStatusCode();
                    }

                    if (notificationMessage.ChannelTypes.Contains(ChannelType.SMS))
                    {
                        var smsMessageRequest = new OrderSMSRequestModel
                        {
                            Order = notificationMessage.Order
                        };

                        _httpClient.BaseAddress = new Uri(_appSettings.NotificationServiceAPIUrl);
                        var smsResult = await _retryPolicy.ExecuteAsync(() =>
                            _httpClient.PutAsJsonAsync("api/notification/send-sms", smsMessageRequest));

                        smsResult.EnsureSuccessStatusCode();

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
