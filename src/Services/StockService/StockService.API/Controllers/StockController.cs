using Microsoft.AspNetCore.Mvc;
using StockService.Application.Contracts;
using StockService.Application.Models.Requests;

namespace StockService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpPut, Route("update-stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequestModel requestModel)
    {
        var result = await _stockService.UpdateStockAsync(requestModel);

        if (!result.Successed)
            return NotFound();

        return NoContent();
    }


}