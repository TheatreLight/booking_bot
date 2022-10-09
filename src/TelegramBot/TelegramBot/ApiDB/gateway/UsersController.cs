using ApiDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDB.dal.Repositories;
using ApiDB.dal.Interface;

namespace ApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller 
    {
        
        AppDBContext db;
        public UsersController(AppDBContext context)
        {
            db = context;
        
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.User.ToList();
        }



        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.User.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
        [HttpGet("name/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            var users = from m in db.User
                         select m;
            var user = await users.Where(s => s.UserLogin.StartsWith(username)).ToListAsync();

            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
        [HttpGet("campus/{campus}")]
        public async Task<IActionResult> GetCampus(int campus)
        {
            var users = from m in db.User
                        select m;
            var user = await users.Where(s => s.Campus == campus).ToListAsync();

            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.User.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.User.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}

