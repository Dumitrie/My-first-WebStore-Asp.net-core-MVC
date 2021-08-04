using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUserModel> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<AppUserModel> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index() => View(_roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AppRoleCreateModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Roles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Error", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("Error");
            }

            var members = new List<AppUserModel>();
            var nonMembers = new List<AppUserModel>();
            foreach (var user in _userManager.Users)
            {
                bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (isInRole) members.Add(user);
                else nonMembers.Add(user);
            }

            var model = new AppRoleEditModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Modify(AppRoleModifyModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd)
                {
                    AppUserModel user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        IdentityResult result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("Error", error.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToDelete)
                {
                    AppUserModel user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        IdentityResult result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("Error", error.Description);
                            }
                        }
                    }
                }

                return RedirectToAction("Index", "Roles");
            }

            return await Edit(model.RoleId);
        }
    }
}
