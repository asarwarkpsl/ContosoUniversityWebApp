using Data.Repository;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;

        public CoursesController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllUsers()
        {
            return Ok(await _courseRepo.GetCoursesAsync());
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourseAsync(int id)
        {
            var course = await _courseRepo.GetCourseAsync(id);

            if (course == null)
                return NotFound();


            return Ok(course);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Course newCourse)
        {
            await _courseRepo.AddAsync(newCourse);
            await _courseRepo.SaveAsync();

            return CreatedAtAction(nameof(GetCourseAsync), new { id = newCourse.CourseID }, newCourse);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Course course)
        {
            if (id != course.CourseID)
                return BadRequest();


            _courseRepo.Update(course);
            await _courseRepo.SaveAsync();


            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetCourseAsync(id);

            if (course == null)
                return NotFound();


            _courseRepo.Delete(id);
            await _courseRepo.SaveAsync();

            return NoContent();
        }
    }
}
