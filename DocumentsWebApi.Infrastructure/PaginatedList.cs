using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi.Infrastructure
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; } = new List<T>();
      
        public int PageNumber { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
           
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;

            Items.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
