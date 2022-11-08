using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Data.Repository;
using Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _repo;

        public EnrollmentsController(IEnrollmentRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<EnrollmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> Get()
        {
            return Ok(await _repo.GetEnrollmentsAsync());
        }

        // GET api/<EnrollmentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentByID(int id)
        {
            var enrollment = await _repo.GetEnrollmentAsync(id);

            if (enrollment == null)
                return NotFound();

            return Ok(enrollment);
        }

        // POST api/<EnrollmentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Enrollment newEnrollment)
        {
            await _repo.AddAsync(newEnrollment);
            await _repo.SaveAsync();

            return CreatedAtAction(nameof(GetEnrollmentByID), new { id = newEnrollment.ID }, newEnrollment);
        }
        
        // PUT api/<EnrollmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Enrollment enrollment)
        {
            if (id != enrollment.ID)
                return BadRequest();

            _repo.Update(enrollment);
            await _repo.SaveAsync();

            return NoContent();
        }

        // DELETE api/<EnrollmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _repo.Delete(id);
            await _repo.SaveAsync();

            return NoContent();
        }
    }
}
