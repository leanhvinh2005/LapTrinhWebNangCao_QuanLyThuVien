using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        [Route("/Profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
