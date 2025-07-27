using StockService.Application.Models.Requests;
using StockService.Application.Models.Responses;
namespace StockService.Application.Contracts;

public interface IStockService
{
    Task<ServiceResponse> UpdateStockAsync(UpdateStockRequestModel requestModel);

}