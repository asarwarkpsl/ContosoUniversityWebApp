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

namespace ContosoUniversity.Web.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly IStudentRepository _studentRepo;

        public DetailsModel(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

      public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                if(_studentRepo.StudentExistsAsync((int)id).Result == false)
                    return NotFound();  
            }
            var student = await _context.Students
                                        .Include(s => s.Enrollments)
                                        .ThenInclude(e => e.Course)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                Student = student;
            }
            return Page();
        }
    }
}
