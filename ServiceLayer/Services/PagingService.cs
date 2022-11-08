using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public static class PagingService
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int pageNumZeroStart, int pageSize)
        {
            if (pageSize == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize cannot be 0");
            }
            if (pageNumZeroStart != 0)
            {
                query = query.Skip((pageNumZeroStart - 1) * pageSize);
            }
            return query.Take(pageSize);
        }
    }
}
