using ContosoUniversity.Data.Models;
using ContosoUniversity.Data.Repository;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Web.Pages.Instructors
{
    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(IInstructorRepository repo,
                                               Instructor instructor)
        {
            //var allCourses = context.Courses;
            var allCourses = repo.GetCourses();
            var instructorCourses = new HashSet<int>(
                instructor.Courses.Select(c => c.CourseID));
            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
        }
    }
}
