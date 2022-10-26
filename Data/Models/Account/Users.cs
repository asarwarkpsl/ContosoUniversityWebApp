using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContosoUniversity.Data.Models.Account
{
    public class Users
    {
        [Required(ErrorMessage = "Please Enter User name")]
        [MinLength(5, ErrorMessage = "User Name must me minimum 5 characters long")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password")]
        [MinLength(5, ErrorMessage = "Password must me minimum 5 characters long")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool Rememberme { get; set; }
    }
}
