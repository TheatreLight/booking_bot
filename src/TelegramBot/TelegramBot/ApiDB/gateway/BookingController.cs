using ApiDB.dal;
using ApiDB.Model;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        AppDBContext db;
        public BookingController(AppDBContext context)
        {
            db = context;
        }
        [HttpGet]
        public IEnumerable<Booking> Get()
        {
            return db.Booking.ToList();
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Get(int id)
        {
            Booking booking = db.Booking.FirstOrDefault(x => x.Id == id);
            if (booking == null)
                return NotFound();
            return new ObjectResult(booking);
        }

      
        [HttpGet("date/{date:length(24)}")]
        public IActionResult GetUserId(string date)
        {
            var booking = db.Booking.FirstOrDefault(x => x.Equals(date.ToLower()));
            if (booking == null)
                return NotFound();
            return (IActionResult)booking;
        }


        // GET api/users/5
        [HttpGet("user/{id:length(24)}")]
        public IActionResult GetUserId(int id)
        {
            var booking = db.Booking.FirstOrDefault(x => x.UserId == id);
            if (booking == null)
                return NotFound();
            return (IActionResult)booking;
        }

            [HttpGet("subject/{id:length(24)}")]
        public IActionResult GetSubjectId(int id)
        {
            Booking booking = db.Booking.FirstOrDefault(x => x.SubjectId == id);
            if (booking == null)
                return NotFound();
            return new ObjectResult(db.Booking.ToList());
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Subject>> Post(Booking booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }

            db.Booking.Add(booking);
            await db.SaveChangesAsync();
            return Ok(booking);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<User>> Put(Booking booking)
        {
            if (booking == null)
            {
                return BadRequest();
            }
            if (!db.Booking.Any(x => x.TimeFrom == booking.TimeFrom && x.Subject == booking.Subject))
            {
                return NotFound();
            }

            db.Update(booking);
            await db.SaveChangesAsync();
            return Ok(booking);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> Delete(int id)
        {
            Booking booking = db.Booking.FirstOrDefault(x => x.UserId == id);
            if (booking == null)
            {
                return NotFound();
            }
            db.Booking.Remove(booking);
            await db.SaveChangesAsync();
            return Ok(booking);
        }

    }
    
}
