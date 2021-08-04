using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DataAccessLayer;

namespace WebStore.Components
{
    public class MenuCategoriesViewComponent : ViewComponent
    {
        private readonly IProductRepository _repository;

        public MenuCategoriesViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categories =_repository
                .Products
                .Select(p => p.Category)
                .Distinct()
                .ToList();
            return View(categories);
        }
    }
}
