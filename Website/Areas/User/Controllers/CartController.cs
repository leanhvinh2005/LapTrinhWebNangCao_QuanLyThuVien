using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public CartController(ApplicationDbContext context)
        {
            _context = context;
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
