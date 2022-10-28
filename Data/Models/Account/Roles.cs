using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Models.Account
{
    public class Roles
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; } = true;

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
