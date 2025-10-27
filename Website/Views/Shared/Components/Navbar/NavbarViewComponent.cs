using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Website.Models.ViewModels;

namespace Website.Views.Shared.Components.Navbar
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NavbarViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                string? role = user.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "Admin")
                    return View("Admin");
                else
                {
                    var session = _httpContextAccessor.HttpContext.Session;
                    var cartJson = session.GetString("Cart");

                    CartListViewModel cart;
                    if (string.IsNullOrEmpty(cartJson))
                        cart = new CartListViewModel();
                    else
                        cart = JsonConvert.DeserializeObject<CartListViewModel>(cartJson) ?? new CartListViewModel();
                    return View("User", cart);
                }               
            }
            return View("NoLogin");
        }
    }
}