using System;
namespace NotificationService.Messaging;

public class NotificationQueueConsumer : IDisposable
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "stock_queue";
    private IConnection? _connection;
    private IChannel _channel;
    private readonly HttpClient _httpClient;

    public NotificationQueueConsumer()
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
            var notificationMessage = JsonSerializer.Deserialize<NotificationMessage>(message);

            // Örnek: Mesajı işleme simülasyonu
            if (notificationMessage != null)
            {
                Console.WriteLine($"OrderId: {notificationMessage.Order.Id}, Items count: {notificationMessage.Order.Items.Count}");

                if (notificationMessage.ChannelType.Email)
                {
                    var response = await _httpClient.PutAsJsonAsync("api/notification/send-email", stockUpdate);
                }

                if (notificationMessage.ChannelType.SMS)
                {
                    var response = await _httpClient.PutAsJsonAsync("api/notification/send-sms", stockUpdate);

                }
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
