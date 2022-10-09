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
    public class BookingController : Controller
    {
        ApplicationContext db;

        public BookingController(ApplicationContext context)
        {
            db = context;
        }

        //private readonly ApplicationContext _context;

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            return View(await db.Booking.ToListAsync());
        }
    }
}
