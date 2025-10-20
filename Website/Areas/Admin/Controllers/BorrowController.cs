using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BorrowController : Controller
    {
        [Route("/Borrow")]
        public IActionResult Borrow()
        {
            return View();
        }
    }
}
