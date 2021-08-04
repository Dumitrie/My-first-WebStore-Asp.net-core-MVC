using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DataAccessLayer;
using WebStore.Domeins;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        private readonly int _pageSize = 5;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        public IActionResult ProductsView(string category, int pageNumber = 1)
        {
            var products = _repository
                .Products
                .Where(p => category == null || p.Category == category)
                .Skip((pageNumber - 1) * _pageSize)
                .Take(_pageSize);

            var info = new PaginatorInfo()
            {
                PageSize = this._pageSize,
                CurrentPage = pageNumber,
                TotalItems = _repository.Products
                    .Count(p => category == null || p.Category == category)
            };

            var model = new ProductPageModel()
            {
                Products = products,
                Info = info,
                Category = category
            };
            return View(model);
        }
    }
}
