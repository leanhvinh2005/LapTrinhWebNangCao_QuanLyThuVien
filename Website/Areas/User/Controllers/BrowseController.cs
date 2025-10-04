using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class BrowseController : Controller
    {
        public IActionResult Browse()
        {
            return View();
        }
    }
}
