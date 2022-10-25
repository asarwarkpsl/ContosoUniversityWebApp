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

namespace ContosoUniversity.Web.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public DeleteModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
      public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _repository.GetDepartmentAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            else 
            {
                Department = department;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department =  await _repository.GetDepartmentAsync(id);

            if (department != null)
            {
                Department = department;
                
                _repository.Delete(Department.ID);
                await _repository.SaveAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
