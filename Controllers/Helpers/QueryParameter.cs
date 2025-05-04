using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Web_API.config.Helpers
{
    public class QueryParameter
    {
        private const int maxPagesize = 50;
        // [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 6, [FromQuery] string? search = null, [FromQuery] string? sortOrder = null
        public int PageNumber { get; set; } = 1; //default pagenumber is 1 and it's dynamices 
        public int PageSize { get; set; } = 6;
        public string? search { get; set; }
        public string? sortOrder { get; set; }

        public QueryParameter Validate()
        {
            if (PageNumber < 1) PageNumber = 1;
            if (PageSize < 1) PageSize = 6;
            if (PageSize > maxPagesize) PageSize = maxPagesize;
            return this;
        }

    }
}