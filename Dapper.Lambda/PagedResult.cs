using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Lambda
{
    public class PagedResult<T> where T : class
    {
        public PagedResult(IEnumerable<T> resultList, int resultCount,int pageSize,int pageNumber)
        {
            Results = resultList;
            Count = resultCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
        public IEnumerable<T> Results { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
