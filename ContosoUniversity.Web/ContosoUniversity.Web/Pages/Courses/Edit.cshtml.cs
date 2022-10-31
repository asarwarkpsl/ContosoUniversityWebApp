using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using Data.Repository;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Courses
{

    [Authorize(Policy ="Admin")]
    public class EditModel : DepartmentNamePageModel
    {
        private readonly ICourseRepository _repository;

        public EditModel(ICourseRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _repository.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            Course = course;

            PopulateDepartmentsDropDownList(_repository, Course.DepartmentID);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _repository.Update(Course);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.CourseID))
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

        private bool CourseExists(int id)
        {
            return _repository.CourseExists(id);
        }
    }
}
