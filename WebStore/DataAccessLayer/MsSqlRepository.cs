using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domeins;

namespace WebStore.DataAccessLayer
{
    public class MsSqlRepository: IProductRepository
    {
        private ApplicationDbContext _context;
        public MsSqlRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
