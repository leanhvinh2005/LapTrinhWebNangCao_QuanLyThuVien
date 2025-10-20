using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]    
    public class CardController : Controller
    {
        [Route("/Card")]
        public IActionResult Card()
        {
            return View();
        }
    }
}
