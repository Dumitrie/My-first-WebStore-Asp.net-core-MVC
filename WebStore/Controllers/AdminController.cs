using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;

        public AdminController(UserManager<AppUserModel> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUserModel user = new AppUserModel()
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.TryAddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Index()
        {
            var users =_userManager.Users.ToList();
            
            return View(users);
        }
    }
}
