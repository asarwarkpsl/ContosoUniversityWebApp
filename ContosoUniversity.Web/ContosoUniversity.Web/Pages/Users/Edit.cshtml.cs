using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data.Models.Account;
using Data.Context;
using Microsoft.AspNetCore.Authorization;
using ContosoUniversity.Data.Models;
using ContosoUniversity.Data.Repository;
using NuGet.Packaging;

namespace ContosoUniversity.Web.Pages.Users
{
    [Authorize(Policy = "Admin")]
    public class EditModel : UserRolesPageModel
    {
        private readonly IAccountRepository _accountRepo;

        public EditModel(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public List<AssignedUserRoles> UserRoles { get; set; }


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
            User = user;

            //Populate assigned roles
            UserRoles = PopulateAssignedUserRoles(_accountRepo, User);


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id,int[] assignedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _accountRepo.getUserByID(id);

            user.UserRoles = null;
            user.UserRoles = new List<UserRoles>();
            User = user;


            var allRoles = _accountRepo.getAllRoles();
            //Unique roles
            var selectedRolesHS = new HashSet<int>(assignedRoles);


            foreach (var role in selectedRolesHS)
            {
                UserRoles newRole = new()
                {
                    Roles = allRoles.Find(r => r.ID == role)
                };

                User.UserRoles.Add(newRole);
            }

            _accountRepo.UpdateUser(User);

            try
            {
                _accountRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! _accountRepo.IsUserExists(User.UserName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
