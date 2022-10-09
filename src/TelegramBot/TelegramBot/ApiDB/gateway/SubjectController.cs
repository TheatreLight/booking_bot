using ApiDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        AppDBContext db;
        public SubjectController(AppDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public IEnumerable<Subject> Get()
        {
            return db.Subject.ToList();
        }

        [HttpGet("sec/{sec}")]
        public async Task<IActionResult> GetSecAsync(string sec)
        {
            var subjects = from m in db.Subject
                        select m;
            string std = "student";
            string stf = "staff";
            if (std.Equals(sec.ToLower()))
            {
                var subject = await subjects.Where(s => s.securType==0).ToListAsync();
                return new JsonResult(subject);
            }
            else if (stf.Equals(sec.ToLower()))
            {
                var subject = await subjects.Where(s => s.securType <= 1).ToListAsync();
                return new JsonResult(subject);
            }
            else 
            {
                return NotFound();
            }
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Subject subject = db.Subject.FirstOrDefault(x => x.Id == id);
            if (subject == null)
                return NotFound();
            return new ObjectResult(subject);
        }
        [HttpGet("name/{subjectname}")]
        public async Task<IActionResult> Get(string subjectname)
        {
            var users = from m in db.Subject
                        select m;
            var user = await users.Where(s => s.NameSubject.ToLower().StartsWith(subjectname)).ToListAsync();

            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }
        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Subject>> Post(Subject subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }

            db.Subject.Add(subject);
            await db.SaveChangesAsync();
            return Ok(subject);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<User>> Put(Subject subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            if (!db.Subject.Any(x => x.Id == subject.Id))
            {
                return NotFound();
            }

            db.Update(subject);
            await db.SaveChangesAsync();
            return Ok(subject);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> Delete(int id)
        {
            Subject subject = db.Subject.FirstOrDefault(x => x.Id == id);
            if (subject == null)
            {
                return NotFound();
            }
            db.Subject.Remove(subject);
            await db.SaveChangesAsync();
            return Ok(subject);
        }
    }
    
}
