using ServiceLayer.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class SortFilterOptions
    {
        //Ordering
        public OrderByOptions OrderByOptions { get; set; }

        //Paging
        public const int DefaultPageSize = 2;

        public int PageNum { get; set; }
        public int PageSize { get; set; } = DefaultPageSize;
        public int NumPages { get; set; }



    }
}
