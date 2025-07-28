using System.Text;
using System.Text.Json;
using OrderService.Application.Contracts;
using OrderService.Application.Models;
using RabbitMQ.Client;

namespace OrderService.Application.Services;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{
    private IConnection? _connection;
    private IChannel _channel;

    private async Task StockQueueConfiguration()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            ClientProvidedName = "OrderServicePublisher"
        };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        // Kuyruğu declare et (durable: kalıcı, exclusive: sadece bu bağlantı için, autoDelete: otomatik silme)
        await _channel.QueueDeclareAsync(
            queue: "stock_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public async Task PublishStockUpdateAsync(StockUpdateMessage message)
    {
        await StockQueueConfiguration();
        if (_channel == null)
            throw new InvalidOperationException("RabbitMQ channel is not created. Call ConnectAsync first.");

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        //Exchange boş bırakılırsa default exchange'e gider.
        await _channel.BasicPublishAsync(
            "",
            routingKey: "stock_queue",
            mandatory: false,
            properties,
            body: body
        );
    }

    private async Task NotificaitonQueueConfiguration()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            ClientProvidedName = "OrderServicePublisher"
        };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        // Kuyruğu declare et (durable: kalıcı, exclusive: sadece bu bağlantı için, autoDelete: otomatik silme)
        await _channel.QueueDeclareAsync(
            queue: "stock_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public async Task PublishNotificaitionRequestAsync(NotificationMessage message)
    {
        await NotificaitonQueueConfiguration();
        if (_channel == null)
            throw new InvalidOperationException("RabbitMQ channel is not created. Call ConnectAsync first.");

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        //Exchange boş bırakılırsa default exchange'e gider.
        await _channel.BasicPublishAsync(
            "",
            routingKey: "notification-queue",
            mandatory: false,
            properties,
            body: body
        );
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            _channel.Dispose();
        }
        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection.Dispose();
        }
    }
}