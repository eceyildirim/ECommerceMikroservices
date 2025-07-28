using Newtonsoft.Json;
namespace NotificationService.Application.Models.Responses;

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