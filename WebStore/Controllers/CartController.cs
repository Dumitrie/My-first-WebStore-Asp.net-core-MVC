using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.BusinessLogic;
using WebStore.DataAccessLayer;
using WebStore.Domeins;
using WebStore.Helpers;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly Cart _cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            _cart = cartService;
        }

        public IActionResult Index()
        {
            _cart.ReturUrl = HttpContext.Request.PathAndQuery();
            return View(_cart);
        }

        public RedirectResult AddToCart(int Id, string returnUrl)
        {
            Product product = _repository
                .Products
                .FirstOrDefault(p => p.Id == Id);
            if (product == null)
            {
                return Redirect("Error");
            }

            _cart.ReturUrl = returnUrl;
            _cart.Add(product);

            return Redirect(returnUrl);
        }

        public RedirectResult RemoveFromCart(int Id, string returnUrl)
        {
            Product product = _repository
                .Products
                .FirstOrDefault(p => p.Id == Id);
            if (product == null)
            {
                return Redirect("Error");
            }

            _cart.ReturUrl = returnUrl;
            _cart.Remove(product);

            return Redirect(returnUrl);
        }

        public RedirectResult ClearCart(string returnUrl)
        {
            _cart.ReturUrl = returnUrl;
            _cart.Clear();

            return Redirect(returnUrl);
        }
    }
}
