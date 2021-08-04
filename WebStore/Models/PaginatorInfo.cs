using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class PaginatorInfo
    {
        public int CurrentPage { get; set; }

        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int PagesCount => (int)
            (Math.Ceiling(TotalItems / (float)PageSize));
    }
}
