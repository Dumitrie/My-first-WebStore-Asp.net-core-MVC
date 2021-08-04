using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class AppRoleCreateModel
    {
        [Required]
        [Display(Name = "Наименование роли")]
        public string RoleName { get; set; }
    }
}
