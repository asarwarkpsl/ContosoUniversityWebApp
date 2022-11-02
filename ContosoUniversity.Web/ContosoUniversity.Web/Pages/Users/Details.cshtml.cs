using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data.Models.Account;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using ContosoUniversity.Data.Repository;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Web.Pages.Users
{
    [Authorize(Policy = "Admin")]

    public class DetailsModel : UserRolesPageModel
    {
        private readonly IAccountRepository _accountRepo;
        public List<AssignedUserRoles> AssignedUserRoles { get; set; }

        public DetailsModel(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

      public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var user = _accountRepo.getUserByID(id);

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
                AssignedUserRoles = PopulateAssignedUserRoles(_accountRepo, User);
            }
            return Page();
        }
    }
}
