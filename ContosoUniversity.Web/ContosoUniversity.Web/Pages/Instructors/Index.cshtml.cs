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
using ContosoUniversity.Data.Models;
using System.Runtime.InteropServices;

namespace ContosoUniversity.Web.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly IInstructorRepository _repository;
        public InstructorIndexData InstructorData { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public IndexModel(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public IList<Instructor> Instructor { get;set; } = default!;

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData();

            // if (_context.Instructors != null)
            {
                InstructorData.Instructors = (IList<Instructor>)await _repository.GetInstructorsAsync();
                
            }

            if (id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == id.Value).Single();
                InstructorData.Courses = instructor.Courses;
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                IEnumerable<Enrollment> Enrollments = await _repository.GetEnrollmentsAsync(CourseID);
                InstructorData.Enrollments = Enrollments;
            }
        }
    }
}
