namespace Notification.Application.Models.Responses;

public class ServiceResponse
{
    public bool Successed { get; set; } = true;

    public short Code { get; set; }

    public string Message { get; set; }

    public dynamic Errors { get; set; }

}

public class ServiceResponse<T> : ServiceResponse
{
    public T Result { get; set; }

    public int TotalItems { get; set; } = 0;
}