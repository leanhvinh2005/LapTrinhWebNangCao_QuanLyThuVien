using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]    
    public class CollectionDetailController : Controller
    {
        [Route("/Collection/CollectionDetail")]
        public IActionResult CollectionDetail()
        {
            return View();
        }
    }
}
