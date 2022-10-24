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
    public class DetailsModel : PageModel
    {
        private readonly IInstructorRepository _repository;

        public DetailsModel(IInstructorRepository repository)
        {
            _repository = repository;
        }

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
    }
}
