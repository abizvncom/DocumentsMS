using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected int GetPageNumber()
        {
            var pageNumberString = Request.Query["page_number"].FirstOrDefault();

            if (string.IsNullOrEmpty(pageNumberString))
            {
                pageNumberString = Request.Query["pn"].FirstOrDefault();
            }

            return TryParseIntegerPagination(pageNumberString, 1);
        }

        protected int GetPageSize()
        {
            var pageSizeString = Request.Query["page_size"].FirstOrDefault();

            if (string.IsNullOrEmpty(pageSizeString))
            {
                pageSizeString = Request.Query["ps"].FirstOrDefault();
            }

            return TryParseIntegerPagination(pageSizeString, 10);
        }

        protected int TryParseIntegerPagination(string? value, int defaultValue)
        {
            if (int.TryParse(value, out int result))
            {
                if (result > 0)
                {
                    return result;
                }
            }

            return defaultValue;
        }
    }
}
