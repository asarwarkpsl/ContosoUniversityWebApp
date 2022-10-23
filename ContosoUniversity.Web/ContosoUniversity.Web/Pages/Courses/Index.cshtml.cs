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
using NuGet.Protocol.Core.Types;

namespace ContosoUniversity.Web.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseRepository _repository;

        public IndexModel(ICourseRepository repository)
        {
            _repository = repository;
        }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync()
        {
           // if (_context.Courses != null)
            {
                Course = (IList<Course>)await _repository.GetCoursesAsync();
            }
        }
    }
}
