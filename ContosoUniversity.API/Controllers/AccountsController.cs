using ContosoUniversity.Data.Models.Account;
using ContosoUniversity.Data.Repository;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;

        public AccountsController(IAccountRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<AccountsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _repo.getUsersAsync());
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            return Ok(_repo.getUserByID(id));
        }

        // POST api/<AccountsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User newUser)
        {
            _repo.AddUser(newUser);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.ID }, newUser);
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            if (id != user.ID)
                return BadRequest();


            _repo.UpdateUser(user);
            await _repo.SaveChangesAsync();


            return NoContent();
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(User user)
        {
            _repo.DeleteUser(user);

            return NoContent();
        }

        public void AssignRoles(User user, Roles[] roles)
        {
            _repo.AssignRoles(user, roles);
        }

        public bool isEmailVerified(User user) 
        {
            return _repo.isEmailVerified(user);
        }
        public bool IsUserExists(string userName) 
        {
            return _repo.IsUserExists(userName);
        }

        public List<Roles> getAllRoles() 
        {
            return _repo.getAllRoles();
        }

        public User Login(string userName, string MD5password) 
        {
            return _repo.Login(userName, MD5password);
        }

        public bool ValidatePassword(string userName, string MD5password) 
        {
            return _repo.ValidatePassword(userName, MD5password);   
        }
    }
}
