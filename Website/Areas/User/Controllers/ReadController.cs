using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website.Data;
using Website.Models;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class ReadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReadController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Read")]
        public async Task<IActionResult> Read(string idbook)
        {
            var chapters = await _context.TRANG
                .Where(c => c.idChapter.Substring(0, 2) == idbook.Substring(0, 2))
                .OrderBy(c => c.numberChapter)
                .ToListAsync();

            return View(chapters);
        }
    }
}
