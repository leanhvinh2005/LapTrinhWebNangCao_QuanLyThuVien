using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Areas.User.Views.Components.CartItem
{
    public class CartItemViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Book book)
        {
            return View("CartItem", book);
        }
    }
}