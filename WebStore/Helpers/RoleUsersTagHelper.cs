using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebStore.Models;

namespace WebStore.Helpers
{
    [HtmlTargetElement("td", Attributes = "role-users")]
    public class RoleUsersTagHelper: TagHelper
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<AppUserModel> _userManager;

        public RoleUsersTagHelper(RoleManager<IdentityRole> roleManager, UserManager<AppUserModel> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("role-users")]
        public string Role{ get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var names = new List<string>();
            IdentityRole role =await _roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                foreach (var user in _userManager.Users)
                {
                    bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                    if (isInRole)
                    {
                        names.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ? "нет пользователей": string.Join(", ", names));
        }

    }
}
