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
using Microsoft.Data.SqlClient;

namespace ContosoUniversity.Web.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentRepository _studentRepo;

        [BindProperty()]
        public string? NameSort { get; set; }

        [BindProperty()]
        public string? CurrentFilter { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } = 1;
        public IndexModel(IStudentRepository studentRepo,IConfiguration config)
        {
            _studentRepo = studentRepo;
            _config = config;
        }

        public IList<Student> Student { get;set; } = default!;
        public IConfiguration _config { get; }

        public async Task OnGetAsync(string? sortOrder, string? searchString,int? pageIndex)
        {
            NameSort = string.IsNullOrEmpty(sortOrder) ? "asc" : "desc";
            CurrentFilter = string.IsNullOrEmpty(searchString) ? string.Empty : searchString;
            CurrentPage = (int)(pageIndex != null ? pageIndex : 1);

            var pageSize = _config.GetValue("PageSize", 5);


            (Student, int totalCount) = await _studentRepo.GetStudentsAsync(CurrentFilter, CurrentPage, pageSize);

            if(Student == null)
                return;

            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            IQueryable<Student>? collection = Student.AsQueryable<Student>();

            if (collection == null)
                return;

            switch (NameSort)
            {
                case "desc":
                    collection = collection.OrderByDescending(s => s.LastName);
                    break;
                default:
                    collection = collection.OrderBy(s => s.LastName); break;
            }

            Student = collection.AsNoTracking().ToList();
        }
        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
