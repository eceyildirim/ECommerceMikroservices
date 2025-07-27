using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Contracts;
using OrderService.Application.Models.Requests;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : BaseController<OrderController>
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetOrderById(string id)
    {
        var response = await _orderService.GetOrderById(Guid.Parse(id));

        if (response == null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequestModel orderRequestModel)
    {
        var response = await _orderService.CreateOrderAsync(orderRequestModel);

        return CreatedAtAction(nameof(GetOrderById), new { id = response.Id }, response);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderRequestModel orderRequestModel)
    {
        var updated = await _orderService.UpdateOrderAsync(id, orderRequestModel);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var deleted = await _orderService.DeleteOrderAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

