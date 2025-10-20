using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]    
    public class AuthorDetailController : Controller
    {
        [Route("/Author/AuthorDetail")]
        public IActionResult AuthorDetail()
        {
            return View();
        }
    }
}
