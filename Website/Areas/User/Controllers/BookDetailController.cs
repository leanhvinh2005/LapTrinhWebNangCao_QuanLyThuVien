using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Website.Data;
using Website.Models;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class BookDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;

        public BookDetailController(ApplicationDbContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        [Route("/BookDetail")]
        public async Task<IActionResult> BookDetail(string idbook)
        {
            var book = await _context.SACH
                .Where(b => b.idBook == idbook)
                .FirstOrDefaultAsync();

            var availablecopies = await _bookService.GetAllBooksMatch(idbook.Substring(0, 2), "AVAILABLE");
            var borrowingcopies = await _bookService.GetAllBooksMatch(idbook.Substring(0, 2), "BORROWING");

            ViewData["Available"] = availablecopies.Count;
            ViewData["Borrowing"] = borrowingcopies.Count;

            return View(book);
        }
    }
}
