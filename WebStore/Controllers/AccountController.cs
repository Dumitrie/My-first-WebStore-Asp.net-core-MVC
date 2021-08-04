using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;

        private SignInManager<AppUserModel> _signInManager;
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                AppUserModel user = await _userManager.FindByEmailAsync(model.Email);
                //_userManager.SupportsUserPhoneNumber();
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("Error", "Invalid user email or password");
                }
            }
            return View();
        }
    }
}
