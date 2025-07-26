using OrderService.Domain.Entities;

namespace OrderService.Domain.Contracts;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetOrderWithItemsByIdAsync(Guid id);

    Task RemoveAsync(Order entity);

    Task UpdateAsync(Order entity);
}