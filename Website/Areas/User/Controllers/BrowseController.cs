using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Threading.Tasks;
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
        private readonly BookService _bookController;
        public int itemperpage = 4;

        public BrowseController(ApplicationDbContext context, BookService bookController)
        {
            _context = context;
            _bookController = bookController;
        }

        [Route("/Browse")]
        public async Task<IActionResult> Browse(int currentpage = 1)
        {
            var books = await _bookController.GetAllBooks();

            var skipbooks = books.Skip((currentpage - 1) * itemperpage).Take(itemperpage).ToList();

            BookListViewModel bookList = new BookListViewModel
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
    }
}
