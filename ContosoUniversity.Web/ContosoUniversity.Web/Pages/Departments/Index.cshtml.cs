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
    public class IndexModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public IndexModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //if (_context.Departments != null)
            {
                Department = (IList<Department>)await _repository.GetDepartmentsAsync();
            }
        }
    }
}
