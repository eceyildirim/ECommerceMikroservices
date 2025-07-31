using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Contracts;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Order> GetOrderWithItemsByIdAsync(Guid id)
    {
        return await _context.Set<Order>()
            .Include(o => o.Items)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task RemoveAsync(Order entity)
    {
        entity.IsDeleted = true;
        entity.Items.ForEach(item => item.IsDeleted = true);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        var existingOrder = await _dbSet
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == entity.Id);

        if (existingOrder == null)
            throw new InvalidOperationException("Order bulunamadı.");

        existingOrder.CustomerId = entity.CustomerId;
        existingOrder.OrderDate = entity.OrderDate;
        existingOrder.Status = entity.Status;
        existingOrder.ShippingAddressId = entity.ShippingAddressId;
        existingOrder.TotalAmount = entity.TotalAmount;
        existingOrder.UpdatedAt = DateTime.UtcNow;

        // 1. Mevcut itemlar üzerinde dön, entity.Items'te varsa güncelle, yoksa soft delete yap
        foreach (var existingItem in existingOrder.Items.ToList())
        {
            var updatedItem = entity.Items.FirstOrDefault(i => i.Id == existingItem.Id);
            if (updatedItem != null)
            {
                existingItem.ProductId = updatedItem.ProductId;
                existingItem.Quantity = updatedItem.Quantity;
                existingItem.UnitPrice = updatedItem.UnitPrice;
                existingItem.IsDeleted = updatedItem.IsDeleted;
            }
            else
            {
                existingItem.IsDeleted = true;
            }
        }

        var existingItemIds = existingOrder.Items.Select(i => i.Id).ToList();
        var newItems = entity.Items.Where(i => !existingItemIds.Contains(i.Id));
        foreach (var newItem in newItems)
        {
            existingOrder.Items.Add(newItem);
        }

        await _context.SaveChangesAsync();
    }

}