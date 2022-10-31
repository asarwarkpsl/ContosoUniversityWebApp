using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using Data.Repository;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Courses
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ICourseRepository _repository;

        public DeleteModel(ICourseRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
      public Course Course { get; set; }

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
            else 
            {
                Course = course;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _repository.GetCourseAsync(id);

            if (course != null)
            {
                Course = course;

                _repository.Delete(id);
                await _repository.SaveAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
