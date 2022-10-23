using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.Context;
using Data.Models;
using Data.Repository;
using NuGet.Protocol.Core.Types;

namespace ContosoUniversity.Web.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private ICourseRepository _repository;

        public CreateModel(ICourseRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            ViewData["DepartmentID"] = new SelectList(_repository.GetCoursesAsync().Result, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.AddAsync(Course);
            await _repository.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
