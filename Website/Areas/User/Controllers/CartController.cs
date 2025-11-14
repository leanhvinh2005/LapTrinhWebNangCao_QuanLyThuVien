using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Website.Areas.Admin.Controllers;
using Website.Data;
using Website.Models;
using Website.Models.ViewModels;
using Website.Services;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private const string Session = "Cart";
        private readonly ApplicationDbContext _context;
        private readonly BorrowService _borrowService;

        public CartController(ApplicationDbContext context, BorrowService borrowService)
        {
            _context = context;
            _borrowService = borrowService;
        }

        [Route("/Cart")]
        public IActionResult Cart()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost("/Cart/Add")]
        public async Task<IActionResult> AddToCart(string idbook)
        {
            var book = await _context.SACH
                .FirstOrDefaultAsync(b => b.idBook == idbook);
            var cart = GetCart();
            cart.Books.Add(book);
            SaveCart(cart);

            return RedirectToAction("Browse", "Browse");
        }

        [HttpPost("/Cart/Remove")]
        public IActionResult RemoveFromCart(string idbook)
        {
            var cart = GetCart();
            cart.Books.RemoveAll(b => b.idBook == idbook);
            SaveCart(cart);

            return View("Cart", cart);
        }

        [HttpPost("/Cart/Borrow")]
        public async Task<IActionResult> Borrow(List<string> idbooks)
        {
            foreach (var item in idbooks)
            {
                RemoveFromCart(item);
            }

            var cardid = User.FindFirst("CardId").Value;
            Borrow? borrow = await _context.MUONTRA
                .FromSqlRaw(
                    "SELECT * FROM MUONTRA WHERE idCard = @idcard AND statusBorrow == 'ACTIVE'",
                    new SqlParameter("@idCard", cardid)
                )
                .FirstOrDefaultAsync();

            if (borrow != null)
            {
                foreach (var item in idbooks)
                {
                    await _borrowService.AddBookToBorrow(borrow.idBorrow, item);
                }
            }
            else
            {
                Borrow newborrow = new Borrow
                {
                    idBorrow = 0,
                    dateBorrow = new(),
                    statusBorrow = "PLACEHOLDER",
                    idCard = cardid
                };
                await _borrowService.AddBorrow(newborrow);

                foreach (var item in idbooks)
                {
                    await _borrowService.AddBookToBorrow(newborrow.idBorrow, item);
                }
            }

            return RedirectToAction("Home", "Home");
        }

        private CartListViewModel GetCart()
        {
            var session = HttpContext.Session;
            var cartJson = session.GetString(Session);

            if (string.IsNullOrEmpty(cartJson))
                return new CartListViewModel();

            return JsonConvert.DeserializeObject<CartListViewModel>(cartJson) ?? new CartListViewModel();
        }

        private void SaveCart(CartListViewModel cart)
        {
            var session = HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(Session, cartJson);
        }
    }
}
