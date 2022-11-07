using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repo;

        public StudentsController(IStudentRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return Ok(await _repo.GetStudentsAsync());
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentByID(int id)
        {
            var student = await _repo.GetStudentAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student newStudent)
        {
            await _repo.AddAsync(newStudent);
            await _repo.SaveAsync();

            return CreatedAtAction(nameof(GetStudentByID), new { id = newStudent.ID }, newStudent);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Student student)
        {
            if (id != student.ID)
                return BadRequest();

            _repo.Update(student);
            await _repo.SaveAsync();

            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _repo.Delete(id);
            await _repo.SaveAsync();

            return NoContent();
        }
    }
}
