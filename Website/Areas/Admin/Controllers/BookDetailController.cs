using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]    
    public class BookDetailController : Controller
    {
        [Route("/Book/BookDetail")]
        public IActionResult BookDetail()
        {
            return View();
        }
    }
}
