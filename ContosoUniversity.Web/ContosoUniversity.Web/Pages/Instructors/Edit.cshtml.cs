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

namespace ContosoUniversity.Web.Pages.Instructors
{
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly IInstructorRepository _repository;

        public EditModel(IInstructorRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _repository.GetInstructorAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            Instructor = instructor;
            PopulateAssignedCourseData(_repository, Instructor);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var instructorToUpdate = await _repository.GetInstructorAsync(id);

            if (instructorToUpdate == null)
            {
                return NotFound();
            }


            if (String.IsNullOrWhiteSpace(
                   instructorToUpdate.OfficeAssignment?.Location))
            {
                instructorToUpdate.OfficeAssignment = null;
            }

            UpdateInstructorCourses(selectedCourses, instructorToUpdate);

            _repository.Update(instructorToUpdate);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await InstructorExists(Instructor.ID))
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
        public void UpdateInstructorCourses(string[] selectedCourses,
                                            Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Courses.Select(c => c.CourseID));

            var courses = _repository.GetCourses();

            foreach (var course in courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                    else
                    {
                        if (instructorCourses.Contains(course.CourseID))
                        {
                            var courseToRemove = instructorToUpdate.Courses.Single(
                                                            c => c.CourseID == course.CourseID);
                            instructorToUpdate.Courses.Remove(courseToRemove);
                        }
                    }
                }
            }
        }
        private async Task<bool> InstructorExists(int id)
        {
            return await _repository.isInstructorExists(id);
        }
    }
}
