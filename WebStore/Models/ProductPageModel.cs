using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domeins;

namespace WebStore.Models
{
    public class ProductPageModel
    {
        public IEnumerable<Product> Products { get; set; }

        public PaginatorInfo Info { get; set; }

        public string Category { get; set; }
    }
}
