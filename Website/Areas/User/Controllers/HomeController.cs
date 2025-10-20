using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website.Data;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Home()
        {
            return View();
        }
    }
}
