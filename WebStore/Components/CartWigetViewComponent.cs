using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.BusinessLogic;

namespace WebStore.Components
{
    public class CartWigetViewComponent: ViewComponent
    {
        private readonly Cart _cart;

        public CartWigetViewComponent(Cart cartService)
        {
            _cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
