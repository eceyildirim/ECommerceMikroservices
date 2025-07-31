using StockService.WorkerService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using System.Text;
using StockService.WorkerService.Enums;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Net.Http.Json;
namespace StockService.WorkerService;

public class StockQueueConsumer : BackgroundService
{
    private readonly ILogger<StockQueueConsumer> _logger;
    private readonly AppSettings _appSettings;
    private readonly HttpClient _httpClient;

    public StockQueueConsumer(ILogger<StockQueueConsumer> logger, IOptions<AppSettings> options, IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
        _appSettings = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
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
            await _channel.QueueDeclareAsync(_appSettings.QueueConfiguration.StockQueueName, true, false, false, null);
            await _channel.QueueBindAsync(_appSettings.QueueConfiguration.StockQueueName, _appSettings.QueueConfiguration.ExchangeName, _appSettings.QueueConfiguration.StockRoutingKey);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[StockConsumer] Mesaj alındı: {message}");

                    var stockUpdate = JsonConvert.DeserializeObject<StockUpdateMessage>(message);

                    if (stockUpdate is not null)
                    {
                        _httpClient.BaseAddress = new Uri(_appSettings.StockServiceAPIUrl);
                        var response = await _httpClient.PutAsJsonAsync("api/Stock/update-stock", stockUpdate);

                        if (!response.IsSuccessStatusCode)
                        {
                            var updateOrderRequestModel = new UpdateOrderRequestModel
                            {
                                OrderId = stockUpdate.OrderId,
                                OrderStatus = OrderStatus.OperationalCancelled
                            };

                            _httpClient.BaseAddress = new Uri(_appSettings.OrderServiceAPIUrl);
                            var updateOrderResponse = await _httpClient.PutAsJsonAsync("api/order/update-status", updateOrderRequestModel);
                        }

                        var updateOrderRequestModel = new UpdateOrderRequestModel
                        {
                            OrderId = stockUpdate.OrderId,
                            OrderStatus = OrderStatus.Completed
                        };

                        _httpClient.BaseAddress = new Uri(_appSettings.OrderServiceAPIUrl);
                        var updateOrderResponse = await _httpClient.PutAsJsonAsync("api/order/update-status", updateOrderRequestModel);
                    }

                    // Başarılı işlendiyse RabbitMQ'ya ack gönder
                    await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[StockConsumer] HATA: {ex.Message}");

                    await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                }

                await Task.Yield();
            };


            await _channel.BasicConsumeAsync(
                    queue: _appSettings.QueueConfiguration.StockQueueName,
                    autoAck: false,
                    consumer: consumer);
        }
        catch (Exception exp)
        {
            _logger.LogInformation(exp.Message);
        }


        await Task.Delay(Timeout.Infinite, stoppingToken);

    }

}




