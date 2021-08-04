using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class AppRoleModifyModel
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }

        public string[] IdsToDelete { get; set; } = new string[0];
        public string[] IdsToAdd { get; set; } = new string[0];
    }
}
