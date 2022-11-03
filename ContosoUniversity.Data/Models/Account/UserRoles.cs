using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Models.Account
{
    public class UserRoles
    {
        [Key]
        public int ID { get; set; }
        //public string UserID { get; set; }
        //public string RolesID { get; set; }

        public User User { get; set; }
        public Roles Roles { get; set; }
    }
}
