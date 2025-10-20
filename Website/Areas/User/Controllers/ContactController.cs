using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class ContactController : Controller
    {
        [Route("/Contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
