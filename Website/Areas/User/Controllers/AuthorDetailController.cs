using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class AuthorDetailController : Controller
    {
        [Route("/AuthorDetail")]
        public IActionResult AuthorDetail()
        {
            return View();
        }
    }
}
