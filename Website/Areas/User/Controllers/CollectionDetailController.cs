using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class CollectionDetailController : Controller
    {
        [Route("/Browse/CollectionDetail")]
        public IActionResult CollectionDetail()
        {
            return View();
        }
    }
}
