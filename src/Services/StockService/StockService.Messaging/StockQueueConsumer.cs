using System;
namespace StockService.Messaging;

public class StockQueueConsumer : IDisposable
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "stock_queue";
    private IConnection? _connection;
    private IChannel _channel;
    private readonly HttpClient _httpClient;

    public StockQueueConsumer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = "guest",
            Password = "guest",
            ClientProvidedName = "OrderServicePublisher"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        _channel.QueueDeclare(queue: _queueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000/")
        };
    }

    public void StartConsuming()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message: {message}");

            // Burada mesajı deserialize edip işleyebilirsin
            var stockUpdate = JsonSerializer.Deserialize<StockUpdateMessage>(message);

            // Örnek: Mesajı işleme simülasyonu
            if (stockUpdate != null)
            {
                Console.WriteLine($"OrderId: {stockUpdate.OrderId}, Items count: {stockUpdate.Items.Count}");

                var response = await _httpClient.PutAsJsonAsync("api/stock/update-stock", stockUpdate);

            }

            // Mesaj işlendi olarak işaretle (ack)
            _channel.BasicAck(eventArgs.DeliveryTag, false);

            await Task.CompletedTask;
        };

        _channel.BasicConsume(queue: _queueName,
                              autoAck: false,
                              consumer: consumer);

        Console.WriteLine("Started consuming messages...");
    }


    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}