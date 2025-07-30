using Newtonsoft.Json;
namespace StockService.Common.Models;

public class ValidationResponseModel : BaseResponseModel
{
    public List<string> ValidationErrors { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}