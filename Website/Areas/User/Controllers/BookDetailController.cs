using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class BookDetailController : Controller
    {
        [Route("/BookDetail")]
        public IActionResult BookDetail()
        {
            return View();
        }
    }
}
