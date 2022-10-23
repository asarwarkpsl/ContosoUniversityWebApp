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

namespace ContosoUniversity.Web.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseRepository _repository;

        public DetailsModel(ICourseRepository repository)
        {
            _repository = repository;
        }

      public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var course = await _context.Courses.FirstOrDefaultAsync(m => m.ID == id);
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
    }
}
