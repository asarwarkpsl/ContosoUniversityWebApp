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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Courses
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : DepartmentNamePageModel
    {
        private ICourseRepository _repository;

        public CreateModel(ICourseRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_repository);
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
