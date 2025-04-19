using System.Net.Http.Json;

namespace DocumentsWebApiTests.Common
{
    public static class JsonContentHelper
    {
        public static HttpContent ToJsonHttpContent<T>(this T obj)
        {
            return JsonContent.Create(obj);
        }
    }
}
