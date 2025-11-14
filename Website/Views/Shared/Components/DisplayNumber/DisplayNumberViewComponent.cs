using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Models;

namespace Website.Views.Shared.Components.DisplayNumber
{
    public class DisplayNumberViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            ViewData["Count"] = count;
            return View("DisplayNumber");
        }
    }
}