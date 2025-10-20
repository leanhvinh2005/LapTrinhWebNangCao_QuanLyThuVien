using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class AboutController : Controller
    {
        [Route("/About")]
        public IActionResult About()
        {
            return View();
        }
    }
}
