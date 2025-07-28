using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using OrderService.Application.Contracts;
using OrderService.Application.Models;
using OrderService.Application.Models.Requests;
using OrderService.Domain.Contracts;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitMQPublisherService _rabbitMQPublisherService;
    private readonly IOrderRepository _orderRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;
    public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<Customer> customerRepository, IMapper mapper, IRabbitMQPublisherService rabbitMQPublisherService)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
        _rabbitMQPublisherService = rabbitMQPublisherService;
        _customerRepository = customerRepository;
    }
    public async Task<OrderDto> GetOrderById(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order == null)
            return null;

        return _mapper.Map<OrderDto>(order);

    }

    public async Task<OrderDto> CreateOrderAsync(OrderRequestModel orderRequestModel)
    {
        Order order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            CustomerId = orderRequestModel.CustomerId,
            IsDeleted = false,
            Status = Domain.Enums.OrderStatus.Pending,
            OrderDate = orderRequestModel.OrderDate,
            TotalAmount = orderRequestModel.TotalAmount,
            Items = new List<OrderItem>(),
            ShippingAddressId = orderRequestModel.ShippingAddressId
        };

        foreach (var item in orderRequestModel.Items)
        {
            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        // var orderModel = _mapper.Map<Order>(orderRequestModel);

        await _orderRepository.AddAsync(order);

        await _unitOfWork.SaveChangesAsync();

        var orderDto = _mapper.Map<OrderDto>(order);

        //Sipariş oluşturulduktan sonra stok düşürmek için queue'ya data gönderilir.
        var stockUpdateMessage = new StockUpdateMessage
        {
            OrderId = orderDto.Id,
            Items = orderDto.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        await _rabbitMQPublisherService.PublishStockUpdateAsync(stockUpdateMessage);

        // orderDto.Customer = await _customerRepository.GetByIdAsync(orderRequestModel.CustomerId);

        //TODO:ECE
        // if (orderDto.Customer == null)
        //     return null;

        //Notification için queueya istek at.
        //Hem Email'de hem SMS'de kullanılabilsin.
        // var notificationMessage = new NotificationMessage
        // {
        //     ChannelTypes = new List<ChannelType>
        //     {
        //         ChannelType.Email, ChannelType.SMS
        //     },
        //     Order = orderDto
        // };

        // await _rabbitMQPublisherService.PublishNotificaitionRequestAsync(notificationMessage);

        return orderDto;
    }

    public async Task<bool> UpdateOrderAsync(Guid id, OrderRequestModel model)
    {
        // var currentOrder = await _orderRepository.GetOrderWithItemsByIdAsync(id);
        // if (currentOrder == null)
        //     return false;

        var newOrder = new Order
        {
            Id = id,
            CustomerId = model.CustomerId,
            ShippingAddressId = model.ShippingAddressId,
            Status = model.Status,
            OrderDate = model.OrderDate,
            UpdatedAt = DateTime.UtcNow,
            TotalAmount = model.TotalAmount,
            Items = new List<OrderItem>()
        };

        foreach (var item in model.Items)
        {
            newOrder.Items.Add(
                new OrderItem
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    IsDeleted = item.IsDeleted
                }
            );
        }

        // _mapper.Map(newOrder, currentOrder);
        // _orderRepository.Update(currentOrder);

        // foreach(var item in currentOrder.Items)
        //     _orderItemRepository.Update(item);

        await _orderRepository.UpdateAsync(newOrder);

        return true;
    }

    public async Task UpdateStatusAsync(UpdateOrderRequestModel model)
    {
        var currentOrder = await _orderRepository.GetOrderWithItemsByIdAsync(model.OrderId);

        currentOrder.OrderStatus = model.OrderStatus;

        await _orderRepository.UpdateAsync(currentOrder);

        //Notification için queueya istek at.
        //Hem Email'de hem SMS'de kullanılabilsin.
        var notificationMessage = new NotificationMessage
        {
            ChannelTypes = new List<ChannelType>
            {
                ChannelType.Email, ChannelType.SMS
            },
            Order = currentOrder
        };

        await _rabbitMQPublisherService.PublishNotificaitionRequestAsync(notificationMessage);
    }

    public async Task<bool> DeleteOrderAsync(Guid id)
    {
        var currentOrder = await _orderRepository.GetOrderWithItemsByIdAsync(id);
        if (currentOrder == null)
            return false;

        await _orderRepository.RemoveAsync(currentOrder);
        await _unitOfWork.CommitTransactionAsync();

        return true;
    }
}