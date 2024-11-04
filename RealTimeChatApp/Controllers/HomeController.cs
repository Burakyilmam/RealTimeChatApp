using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealTimeChatApp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            var username = HttpContext.Request.Cookies["Username"];
            ViewData["Username"] = username;
            return View();
        }
    }
}
