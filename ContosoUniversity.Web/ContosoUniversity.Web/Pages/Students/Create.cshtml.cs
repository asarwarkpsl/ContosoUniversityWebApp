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
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Web.Pages.Students
{
    [Authorize(Policy = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IStudentRepository _studentRepo;

        public CreateModel(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Students.Add(Student);
            //await _context.SaveChangesAsync();

            await _studentRepo.AddAsync(Student);
            await _studentRepo.SaveAsync();

            return RedirectToPage("./Index");
        }
    }
}
