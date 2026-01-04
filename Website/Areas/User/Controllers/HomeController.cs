using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Website.Areas.User.Models;
using Website.Data;
using Website.Models;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;

        public HomeController(ApplicationDbContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        [Route("/")]
        public async Task<IActionResult> Home()
        {
            if (User.IsInRole("User"))
            {
                var books = await _bookService.GetAllBooksUnique();

                var random = new Random();
                var banner = books.OrderBy(x => random.Next()).Take(5).ToList();
                var carousel1 = books.OrderBy(x => random.Next()).Take(8).ToList();
                var carousel2 = books.OrderBy(x => random.Next()).Take(8).ToList();
                var carousel3 = books.OrderBy(x => random.Next()).Take(8).ToList();
                var carousel4 = books.OrderBy(x => random.Next()).Take(8).ToList();

                HomeList homelist = new HomeList
                {
                    Banner = banner,
                    Carousel1 = carousel1,
                    Carousel2 = carousel2,
                    Carousel3 = carousel3,
                    Carousel4 = carousel4
                };

                return View(homelist);
            }
            return View();
        }
    }
}
