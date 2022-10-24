using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;
using ContosoUniversity.Data.Repository;
using NuGet.Protocol.Core.Types;

namespace ContosoUniversity.Web.Pages.Instructors
{
    public class DeleteModel : PageModel
    {
        private readonly IInstructorRepository _repository;

        public DeleteModel(IInstructorRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
      public Instructor? Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var instructor = await _repository.GetInstructorAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }
            else 
            {
                Instructor = instructor;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _repository.GetInstructorAsync(id);

            if (instructor != null)
            {
                Instructor = instructor;

                _repository.Delete(id);
                await _repository.SaveAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
