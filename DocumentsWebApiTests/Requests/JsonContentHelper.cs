using System.Net.Http.Json;

namespace DocumentsWebApiTests.Requests
{
    public static class JsonContentHelper
    {
        public static HttpContent ToJsonHttpContent<T>(this T obj)
        {
            return JsonContent.Create(obj);
        }
    }
}
