﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Models
{
    public class AssignedUserRoles
    {
        public int RoleID { get; set; }
        public string Name{ get; set; }
        public bool isAssigned { get; set; }
    }
}
