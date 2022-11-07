using ContosoUniversity.Data.Repository;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentsController(IDepartmentRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            return Ok(await _repo.GetDepartmentsAsync());
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentByID(int id)
        {
            var department = await _repo.GetDepartmentAsync(id);

            if (department == null)
                return NotFound();

            return department;
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Department newDepartment)
        {
            await _repo.AddAsync(newDepartment);
            await _repo.SaveAsync();

            return CreatedAtAction(nameof(GetDepartmentByID), new { id = newDepartment.ID }, newDepartment);
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Department department)
        {
            if (id != department.ID)
                return BadRequest();

            _repo.Update(department);
            await _repo.SaveAsync();

            return NoContent();
        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _repo.Delete(id);
            await _repo.SaveAsync();

            return NoContent();
        }
    }
}
