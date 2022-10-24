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
    public class IndexModel : PageModel
    {
        private readonly IInstructorRepository _repository;

        public IndexModel(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public IList<Instructor> Instructor { get;set; } = default!;

        public async Task OnGetAsync()
        {
           // if (_context.Instructors != null)
            {
                Instructor = (IList<Instructor>)await _repository.GetInstructorsAsync();
            }
        }
    }
}
