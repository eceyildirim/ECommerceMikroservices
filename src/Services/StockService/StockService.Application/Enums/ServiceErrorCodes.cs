using System.Linq;
namespace StockService.Application.Enums;

public enum ServiceErrorCodes
{
    Ok = 200,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Status500InternalServerError = 500
}