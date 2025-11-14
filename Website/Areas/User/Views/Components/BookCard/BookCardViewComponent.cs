using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Areas.User.Views.Components.BookCard
{
    public class BookCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Book book)
        {
            //ViewData["Count"] = count;
            return View("BookCard", book);
        }
    }
}