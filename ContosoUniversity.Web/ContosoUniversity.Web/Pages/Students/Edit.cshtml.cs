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
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Students
{
    [Authorize(Policy = "Admin,Student")]
    public class EditModel : PageModel
    {
        private readonly IStudentRepository _studentRepo;

        public EditModel(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var student = await _studentRepo.GetStudentAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            Student = student;
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

            //_context.Attach(Student).State = EntityState.Modified;

            try
            {
                _studentRepo.Update(Student);
                await _studentRepo.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if(!await _studentRepo.StudentExistsAsync(Student.ID))
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
