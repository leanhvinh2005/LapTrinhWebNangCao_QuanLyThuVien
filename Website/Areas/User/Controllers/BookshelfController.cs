using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Website.Areas.User.Models;
using Website.Data;
using Website.Models;
using Website.Models.ViewModels;
using Website.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class BookshelfController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;
        private readonly MemberService _memberService;
        public int itemperpage = 12;

        public BookshelfController(ApplicationDbContext context, BookService bookService, MemberService memberService)
        {
            _context = context;
            _bookService = bookService;
            _memberService = memberService;
        }

        [Route("/Bookshelf")]
        public async Task<IActionResult> Bookshelf(int currentpage = 1)
        {
            var cardid = User.FindFirst("CardId").Value;

            Borrow? borrow = await _context.MUONTRA
                .FromSqlRaw(
                    "SELECT * FROM MUONTRA WHERE idCard = @idcard AND statusBorrow = 'ACTIVE'",
                    new SqlParameter("@idcard", cardid)
                )
                .FirstOrDefaultAsync();

            List<BookshelfItem> bookitems = new();

            if (borrow != null)
            {
                var borrows = await _context.JOIN_BOOKBORROW
                    .Where(j => j.idBorrow == borrow.idBorrow)
                    .Where(j => j.statusBookBorrow == "PENDING")
                    .ToListAsync();

                var books = await _bookService.GetAllBooks();
                var bookDict = books.ToDictionary(b => b.idBook);
                
                foreach (var item in borrows)
                {
                    if (bookDict.TryGetValue(item.idBook, out var book))
                    {
                        bookitems.Add(new BookshelfItem
                        {
                            BookBorrow = item,
                            Book = book
                        });
                    }
                }
            }

            BookshelfList bookshelf = new BookshelfList
            {
                BookshelfItems = bookitems,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = currentpage,
                    ItemPerPage = itemperpage,
                    TotalItem = bookitems.Count,
                    TotalPage = (int)Math.Ceiling((double)bookitems.Count() / itemperpage)
                }
            };

            return View(bookshelf);
        }
    }
}
