using Data.Context;
using Data.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Web.Pages.Courses
{
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL { get; set; }

        public async void PopulateDepartmentsDropDownList( ICourseRepository repository,object? selectedDepartment = null)
        {
            var depts = await repository.GetDepartmentsAsync();

            DepartmentNameSL = new SelectList(depts,"ID", "Name", selectedDepartment);
        }
    }
}
