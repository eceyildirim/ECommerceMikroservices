using Newtonsoft.Json;

namespace StockService.Common.Models;

public class BaseResponseModel
{
    public bool IsSuccess { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

}