using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]    
    public class TagController : Controller
    {
        [Route("/Tag")]
        public IActionResult Tag()
        {
            return View();
        }
    }
}
