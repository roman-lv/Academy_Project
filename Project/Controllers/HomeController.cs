using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models.SchoolViewModels;
using Microsoft.AspNetCore.Identity;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(SchoolContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> CreateUsers()
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Student"));

            //Users
            await _userManager.CreateAsync(new User() { UserName = "tim.drake", Name = "Tim Drake", Email = "timdrake@gmail.com" }, "123456");
            await _userManager.CreateAsync(new User() { UserName = "rick.sanchez", Name = "Rick Sanchez", Email = "ricksanchez@gmail.com" }, "123456");


            User tim = await _userManager.FindByNameAsync("tim.drake");
            User rick = await _userManager.FindByNameAsync("rick.sanchez");

            await _userManager.AddToRoleAsync(rick, "Admin");
            await _userManager.AddToRoleAsync(tim, "Student");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
            

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
