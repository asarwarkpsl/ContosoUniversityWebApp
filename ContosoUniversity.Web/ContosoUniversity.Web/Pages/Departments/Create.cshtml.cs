using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.Context;
using Data.Models;
using ContosoUniversity.Data.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Departments
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public CreateModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
        ViewData["InstructorID"] = new SelectList(await _repository.GetInstructorsAsync(), "ID", "FirstMidName");
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.AddAsync(Department);
            await _repository.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
