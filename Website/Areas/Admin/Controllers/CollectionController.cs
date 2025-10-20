using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CollectionController : Controller
    {
        [Route("/Collection")]
        public IActionResult Collection()
        {
            return View();
        }
    }
}
