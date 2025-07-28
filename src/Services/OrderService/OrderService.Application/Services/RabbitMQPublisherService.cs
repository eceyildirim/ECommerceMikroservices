using System.Text;
using System.Text.Json;
using OrderService.Application.Contracts;
using OrderService.Application.Models;
using OrderService.Common;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;

namespace OrderService.Application.Services;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{
    private readonly AppSettingsModel _appSettingsModel;

    public RabbitMQPublisherService(IOptions<AppSettingsModel> appSettingsModel)
    {
        _appSettingsModel = appSettingsModel.Value;
    }

    public async Task PublishStockUpdateAsync(StockUpdateMessage message)
    {
        IConnection? _stockConnection;
        IChannel _stockChannel;

        var stockFactory = new ConnectionFactory()
        {
            HostName = _appSettingsModel.StockQueueConfiguration.HostName,
            UserName = _appSettingsModel.StockQueueConfiguration.UserName,
            Password = _appSettingsModel.StockQueueConfiguration.Password,
            ClientProvidedName = _appSettingsModel.StockQueueConfiguration.ClientProvidedName
        };
        _stockConnection = await stockFactory.CreateConnectionAsync();
        _stockChannel = await _stockConnection.CreateChannelAsync();

        await _stockChannel.QueueDeclareAsync(
            queue: _appSettingsModel.StockQueueConfiguration.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        if (_stockChannel == null)
            throw new InvalidOperationException("RabbitMQ channel is not created. Call ConnectAsync first.");

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        //Exchange boş bırakılırsa default exchange'e gider.
        await _stockChannel.BasicPublishAsync(
            "",
            routingKey: _appSettingsModel.StockQueueConfiguration.QueueName,
            mandatory: false,
            properties,
            body: body
        );


    }

    public async Task PublishNotificaitionRequestAsync(NotificationMessage message)
    {
        IConnection? _notificationConnection;
        IChannel _notificationChannel;

        var notificationFactory = new ConnectionFactory()
        {
            HostName = _appSettingsModel.NotificationQueueConfiguration.HostName,
            UserName = _appSettingsModel.NotificationQueueConfiguration.UserName,
            Password = _appSettingsModel.NotificationQueueConfiguration.Password,
            ClientProvidedName = _appSettingsModel.NotificationQueueConfiguration.ClientProvidedName
        };
        _notificationConnection = await notificationFactory.CreateConnectionAsync();
        _notificationChannel = await _notificationConnection.CreateChannelAsync();

        await _notificationChannel.QueueDeclareAsync(
            queue: _appSettingsModel.NotificationQueueConfiguration.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        if (_notificationChannel == null)
            throw new InvalidOperationException("RabbitMQ channel is not created. Call ConnectAsync first.");

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        //Exchange boş bırakılırsa default exchange'e gider.
        await _notificationChannel.BasicPublishAsync(
            "",
            routingKey: _appSettingsModel.NotificationQueueConfiguration.QueueName,
            mandatory: false,
            properties,
            body: body
        );
    }
}