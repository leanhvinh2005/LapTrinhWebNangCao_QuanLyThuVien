using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Threading.Tasks;
using Website.Areas.User.Models;
using Website.Data;
using Website.Models;
using Website.Models.ViewModels;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class BrowseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;
        public int itemperpage = 12;

        public BrowseController(ApplicationDbContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        [Route("/Browse")]
        public async Task<IActionResult> Browse(int currentpage = 1)
        {
            var books = await _bookService.GetAllBooksUnique();

            var skipbooks = books.Skip((currentpage - 1) * itemperpage).Take(itemperpage).ToList();

            BookList bookList = new BookList
            {
                Books = skipbooks,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentpage,
                    ItemPerPage = itemperpage,
                    TotalItem = books.Count,
                    TotalPage = (int)Math.Ceiling((double)books.Count() / itemperpage)
                }
            };
            
            return View(bookList);
        }

        [Route("/Browse/Search")]
        public async Task<IActionResult> Search(string search, int currentpage = 1)
        {
            var books = await _bookService.GetAllBooksUnique();
            var searchbooks = _bookService.SearchBookUser(books, search);

            var skipbooks = searchbooks.Skip((currentpage - 1) * itemperpage).Take(itemperpage).ToList();

            BookList bookList = new BookList
            {
                Books = skipbooks,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentpage,
                    ItemPerPage = itemperpage,
                    TotalItem = books.Count,
                    TotalPage = (int)Math.Ceiling((double)books.Count() / itemperpage)
                }
            };

            return View("Browse", bookList);
        }

        [HttpPost("/Browse/Filter")]
        public async Task<IActionResult> Filter(List<int> selectedtags, int currentpage = 1)
        {
            var books = await _bookService.FilterBook(selectedtags);

            var skipbooks = books.Skip((currentpage - 1) * itemperpage).Take(itemperpage).ToList();

            BookList bookList = new BookList
            {
                Books = skipbooks,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentpage,
                    ItemPerPage = itemperpage,
                    TotalItem = books.Count,
                    TotalPage = (int)Math.Ceiling((double)books.Count() / itemperpage)
                }
            };

            return View("Browse", bookList);
        }
    }
}
