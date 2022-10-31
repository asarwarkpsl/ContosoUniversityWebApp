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
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Students
{
    [Authorize(Policy = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly IStudentRepository _studentRepo;

        public DeleteModel(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [BindProperty]
      public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           // var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);

            var student = await _studentRepo.GetStudentAsync(id);   

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _studentRepo.GetStudentAsync(id);

            if (student != null)
            {
                Student = student;
                //_context.Students.Remove(Student);
                //await _context.SaveChangesAsync();

                _studentRepo.Delete(student);
                await _studentRepo.SaveAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
