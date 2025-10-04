using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
