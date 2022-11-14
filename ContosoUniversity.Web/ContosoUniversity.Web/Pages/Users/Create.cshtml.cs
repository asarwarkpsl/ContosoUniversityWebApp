using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContosoUniversity.Data.Models.Account;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using ContosoUniversity.Data.Repository;
using Data.Models;
using System.Net;
using ContosoUniversity.Web.Utilities;

namespace ContosoUniversity.Web.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepo;

        public CreateModel(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IActionResult OnGet()
        {
            UserRoles = _accountRepo.getAllRoles();

            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public List<Roles> UserRoles { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int[] SelectedRoles)
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            User.Password = Utility.GenerateMD5(User.Password); 
            User.UserRoles = new List<UserRoles>();
            

            var allRoles = _accountRepo.getAllRoles();
            //Unique roles
            var selectedRolesHS = new HashSet<int>(SelectedRoles);


            foreach (var role in selectedRolesHS)
            {
                UserRoles newRole = new()
                {
                    Roles = allRoles.Find(r => r.ID == role)
                };

                User.UserRoles.Add(newRole);
            }


            _accountRepo.AddUser(User);
            await _accountRepo.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
