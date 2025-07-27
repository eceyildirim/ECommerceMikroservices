using Microsoft.AspNetCore.Mvc;
using StockService.Application.Contracts;
using StockService.Application.Models.Requests;

namespace StockService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : BaseController<StockController>
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    //Stok kontrolü yap ve yeterliyse gelen stok miktarını rezerve et
    // [HttpPost, Route("check-and-reserve-stock")]
    // public async Task<IActionResult> CheckAndReserveStock([FromBody] CheckAndReservceStockRequest request)
    // {
    //     var result = await _stockService.CheckAndReserveStockAsync(request);

    //     if (result == null)
    //         return Conflict(result);

    //     return Ok(result);
    // }

    [HttpPut, Route("update-stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequestModel requestModel)
    {
        var result = await _stockService.UpdateStockAsync(requestModel);

        if (!result.Successed)
            return NotFound();

        return NoContent();
    }


}