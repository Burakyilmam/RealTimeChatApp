using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RealTimeChatApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace RealTimeChatApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("", "Kullanýcý adý boþ olamaz.");
                return View();
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };
            var userIdentity = new ClaimsIdentity(claims, "UserLogin");
            var principal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
