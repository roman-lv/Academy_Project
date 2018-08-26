using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private SchoolContext db;
        public ProfileController(SchoolContext _context, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            db = _context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Info = "Введіть необхідні дані для реєстрації";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = details.UserName,
                    Email = details.Email,
                    Name = details.Name,

                };
                IdentityResult result = await userManager.CreateAsync(user, details.Password);
                IdentityResult result2 = await userManager.AddToRoleAsync(user, "Student");

                if (result?.Succeeded == true && result2?.Succeeded == true)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result1 = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result1.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(details);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Info = "Enter your login and password";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(details.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.UserName), "Неправильне і'мя користувача чи пароль!");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> View(string name)
        {
            User user = db.Users.Where(u => u.UserName == name).First();
            ViewData["name"] = user.Name;
            ViewData["username"] = user.UserName;
            string role = (await userManager.GetRolesAsync(user)).First();
            ViewData["role"] = role;
            return View();
        }
    }
}