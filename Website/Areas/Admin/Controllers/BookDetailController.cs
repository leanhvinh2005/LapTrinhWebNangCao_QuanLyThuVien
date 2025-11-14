using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]    
    public class BookDetailController : Controller
    {
        [Route("/Book/BookDetail")]
        public IActionResult BookDetail(Book book)
        {
            return View(book);
        }
    }
}
