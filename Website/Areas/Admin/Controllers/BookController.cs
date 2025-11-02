using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;
using System.Threading.Tasks;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Book")]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Book(string search)
        {
            search ??= "";
            var books = await _bookService.SearchBook(search);
            ViewData["CurrentSearch"] = search;
            return View(books);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id) 
        {
            var book = await _bookService.GetBookById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Book book)
        {
            if (id != book.idBook) return NotFound();
            if (ModelState.IsValid)
            {
                await _bookService.EditBook(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id) 
        {
            var book = await _bookService.GetBookById(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id) 
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }
}