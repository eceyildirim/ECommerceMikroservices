using System.Text;
using System.Text.Json;
using OrderService.Application.Contracts;
using OrderService.Application.Models;
using RabbitMQ.Client;

namespace OrderService.Application.Services;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{
    private readonly string _hostname = "localhost";
    private readonly string _queueName = "stock_queue";
    private IConnection? _connection;
    private IChannel _channel;

    public async Task ConnectAsync()
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

        // Kuyruğu declare et (durable: kalıcı, exclusive: sadece bu bağlantı için, autoDelete: otomatik silme)
        await _channel.QueueDeclareAsync(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public async Task PublishStockUpdateAsync(StockUpdateMessage message)
    {
        await ConnectAsync();
        if (_channel == null)
            throw new InvalidOperationException("RabbitMQ channel is not created. Call ConnectAsync first.");

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        //Exchange boş bırakılırsa default exchange'e gider.
        await _channel.BasicPublishAsync(
            "",
            routingKey: _queueName,
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