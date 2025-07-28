namespace NotificationService.API.Helpers;

public static class MiddlewareHelper
{
    public static Dictionary<string, string> FormatHeaders(IHeaderDictionary headers)
    {
        Dictionary<string, string> returnValue = new Dictionary<string, string>();

        if (headers != null)
        {
            foreach (var header in headers.Where(x => !string.IsNullOrEmpty(x.Key) || !string.IsNullOrEmpty(x.Value)))
            {
                if (!headers.Keys.Any(x => x == header.Key))
                {
                    headers.Add(header.Key, header.Value);
                }
            }
        }

        return returnValue;
    }
}