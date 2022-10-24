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

namespace ContosoUniversity.Web.Pages.Instructors
{
    public class CreateModel : PageModel
    {
        private readonly IInstructorRepository _repository;

        public CreateModel(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Instructor Instructor { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.AddAsync(Instructor);
            await _repository.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
