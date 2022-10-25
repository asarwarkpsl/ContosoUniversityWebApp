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
using ContosoUniversity.Data.Repository;
using NuGet.Protocol.Core.Types;

namespace ContosoUniversity.Web.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public EditModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Department Department { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department =  await _repository.GetDepartmentAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            Department = department;
           ViewData["InstructorID"] = new SelectList(await _repository.GetInstructorsAsync(), "ID", "FirstMidName");
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

            //_context.Attach(Department).State = EntityState.Modified;
            _repository.Update(Department);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.DepartmentExists(Department.ID))
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
