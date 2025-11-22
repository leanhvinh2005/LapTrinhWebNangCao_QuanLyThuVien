using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Website.Areas.User.Models;
using Website.Data;
using Website.Models;

namespace Website.Services.Other
{
    public class CartService
    {
        private const string Session = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly BorrowService _borrowService;

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public CartService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, BorrowService borrowService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _borrowService = borrowService;
        }

        public async Task AddToCart(string idbook)
        {
            var book = await _context.SACH
                .FirstOrDefaultAsync(b => b.idBook == idbook);
            var cart = GetCart();
            cart.Books.Add(book);
            SaveCart(cart);
        }

        public void RemoveFromCart(List<string> selectedbooks)
        {
            var cart = GetCart();
            foreach (var idbook in selectedbooks)
            {
                cart.Books.RemoveAll(b => b.idBook == idbook);
            }
            SaveCart(cart);
        }

        public async Task BorrowBooks(List<string> selectedbooks)
        {
            RemoveFromCart(selectedbooks);

            var user = _httpContextAccessor.HttpContext.User;
            var cardid = user.FindFirst("CardId").Value;
            Borrow? borrow = await _context.MUONTRA
                .FromSqlRaw(
                    "SELECT * FROM MUONTRA WHERE idCard = @idcard AND statusBorrow = 'ACTIVE'",
                    new SqlParameter("@idcard", cardid)
                )
                .FirstOrDefaultAsync();

            if (borrow != null)
            {
                foreach (var item in selectedbooks)
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
                    idCard = cardid,
                    idLibrarian = null
                };
                int newborrowid = await _borrowService.AddBorrow(newborrow);

                foreach (var item in selectedbooks)
                {
                    await _borrowService.AddBookToBorrow(newborrowid, item);
                }
            }
        }

        public CartList GetCart()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var cartJson = session?.GetString(Session);

            if (string.IsNullOrEmpty(cartJson))
                return new CartList();

            return JsonConvert.DeserializeObject<CartList>(cartJson) ?? new CartList();
        }

        public void SaveCart(CartList cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(Session, cartJson);

            NotifyStateChanged();
        }
    }
}
