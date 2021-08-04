using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebStore.Models
{
    public class AppRoleEditModel
    {
        public IdentityRole Role { get; set; }

        public List<AppUserModel> Members { get; set; }

        public List<AppUserModel> NonMembers { get; set; }
    }
}
