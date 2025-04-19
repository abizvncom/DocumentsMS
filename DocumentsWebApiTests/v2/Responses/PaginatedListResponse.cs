﻿namespace DocumentsWebApiTests.v2.Responses
{
    public sealed record PaginatedListResponse<T>
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public List<T> Items { get; set; } = null!;
    }
}
