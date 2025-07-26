using OrderService.Application.Models;
using OrderService.Application.Models.Requests;

namespace OrderService.Application.Contracts;

public interface IOrderService
{
     Task<OrderDto> GetOrderById(Guid id);
     Task<OrderDto> CreateOrderAsync(OrderRequestModel orderRequestModel);    
     Task<bool> UpdateOrderAsync(Guid id, OrderRequestModel model);
     Task<bool> DeleteOrderAsync(Guid id);
}