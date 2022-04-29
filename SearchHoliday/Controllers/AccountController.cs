using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using SearchHoliday.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace SearchHoliday.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext _db;
        public AccountController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("UserPanel", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("UserPanel", "Account");
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email, user.Role);
                    return RedirectToAction("UserPanel", "Account");
                } 
                else
                {
                    Response.StatusCode = 400;
                    return Json("Не верный email или пароль");
                }
            }
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("UserPanel", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("UserPanel", "Account");
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    _db.Users.Add(new User { Email = model.Email, Password = model.Password, Role = "user" });
                    await _db.SaveChangesAsync();

                    await Authenticate(model.Email, "user");
                    return RedirectToAction("UserPanel", "Account");
                }
                else
                {
                    Response.StatusCode = 400;
                    return Json("Такой пользователь уже существует");
                }
            }
            return View(model);
        }

        private async Task Authenticate(string login, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public IActionResult UserPanel()
        {
            return View();
        }
    }
}
