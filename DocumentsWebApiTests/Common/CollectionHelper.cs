namespace DocumentsWebApiTests.Common
{
    internal static class CollectionHelper
    {
        public static IList<T> TakePage<T>(this IEnumerable<T> sources, int pageNumber, int pageSize)
        {
            return sources.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
