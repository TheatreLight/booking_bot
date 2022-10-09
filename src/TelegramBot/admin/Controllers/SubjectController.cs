using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_Vitalik.Models;

namespace WebApplication_Vitalik.Controllers
{
    public class SubjectController : Controller
    {
        ApplicationContext db;

        public SubjectController(ApplicationContext context)
        {
            db = context;
        }

        //private readonly ApplicationContext _context;

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            return View(await db.Subject.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Subject == null)
            {
                return NotFound();
            }

            var subject = await db.Subject
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameSubject,Level,NumberRoom,Campus,Info,MinTime,Block,Type,securType")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subject.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Subject? subject = await db.Subject.FirstOrDefaultAsync(p => p.Id == id);
                if (subject != null) return View(subject);
            }
            return NotFound();
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameSubject,Level,NumberRoom,Campus,Info,MinTime,Block,Type,securType")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Subject.Update(subject);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(subject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        /* / GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = new User { Id = id.Value };
                db.Entry(user).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }*/

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Subject == null)
            {
                return Problem("Entity set 'ApplicationContext.Users'  is null.");
            }
            var subject = await db.Subject.FindAsync(id);
            if (subject != null)
            {
                db.Subject.Remove(subject);
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (db.Subject?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
