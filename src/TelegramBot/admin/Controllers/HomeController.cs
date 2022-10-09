using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using WebApplication_Vitalik.Models;

namespace WebApplication_Vitalik.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        private readonly UserManager<LoginViewModel> _userManager;
        private readonly SignInManager<LoginViewModel> _signInManager;

        public HomeController(ApplicationContext context)
        {
            db = context;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        //private readonly ApplicationContext _context;

        // GET: Home
        public async Task<IActionResult> Index([FromForm] LoginViewModel? loginForm)
        {
            if (loginForm.login == "vitalik" && loginForm.password == "1122") {
               // await _signInManager.SignInAsync(loginForm, false);
                return RedirectToAction("Index", "User");
                // return Redirect("/User");
            }
            //if (_signInManager.IsSignedIn(loginForm))
            //{
            //    return RedirectToAction("Index", "User");
            //}

            return View();
        }

    }
}
