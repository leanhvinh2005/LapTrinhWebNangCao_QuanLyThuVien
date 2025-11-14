using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Website.Data;
using Website.Models;
using Website.Models.ViewModels;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class BookshelfController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;
        private readonly MemberService _memberService;

        public BookshelfController(ApplicationDbContext context, BookService bookService, MemberService memberService)
        {
            _context = context;
            _bookService = bookService;
            _memberService = memberService;
        }

        [Route("/Bookshelf")]
        public async Task<IActionResult> Bookshelf()
        {
            var cardid = User.FindFirst("CardId").Value;

            Borrow? borrow = await _context.MUONTRA
                .FromSqlRaw(
                    "SELECT * FROM MUONTRA WHERE idCard = @idcard AND statusBorrow == 'ACTIVE'",
                    new SqlParameter("@idCard", cardid)
                )
                .FirstOrDefaultAsync();

            BookshelfInfo bookshelf = new();

            if (borrow != null)
            {
                List<BookBorrow> borrows = await _context.JOIN_BOOKBORROW
                .Where(j => j.idBorrow == borrow.idBorrow)
                .ToListAsync();

                var books = await _bookService.GetAllBooks();
                var bookDict = books.ToDictionary(b => b.idBook);
                List<BookshelfItem> items = new();
                foreach (var item in borrows)
                {
                    if (bookDict.TryGetValue(item.idBook, out var book))
                    {
                        items.Add(new BookshelfItem
                        {
                            BookBorrow = item,
                            Book = book
                        });
                    }
                }

                bookshelf = new BookshelfInfo
                {
                    Items = items
                };
            }

            return View(bookshelf);
        }
    }
}
