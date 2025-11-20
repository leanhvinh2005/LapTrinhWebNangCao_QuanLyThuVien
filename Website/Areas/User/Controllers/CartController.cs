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
using Website.Services.Other;

namespace Website.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [Route("/Cart")]
        public IActionResult Cart()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        [HttpPost("/Cart/Add")]
        public async Task<IActionResult> AddToCart(string idbook)
        {
            await _cartService.AddToCart(idbook);

            return RedirectToAction("Browse", "Browse");
        }
    }
}
