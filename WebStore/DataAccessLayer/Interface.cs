using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domeins;

namespace WebStore.DataAccessLayer
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
