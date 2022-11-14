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

namespace ContosoUniversity.Web.Pages.Users
{
    [Authorize(Policy = "Admin")]

    public class DeleteModel : PageModel
    {
        private readonly IAccountRepository _accountRepo;

        public DeleteModel(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [BindProperty]
      public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _accountRepo.getUserByID(id);

            if (user != null)
            {
                User = user;

                User.Deleted = true;

                _accountRepo.UpdateUser(User);
                await _accountRepo.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
