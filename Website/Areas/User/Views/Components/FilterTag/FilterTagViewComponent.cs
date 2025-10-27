using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Data;
using Website.Models;
using Website.Models.ViewModels;

namespace Website.Areas.User.Views.Components.FilterTag
{
    public class FilterTagViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public FilterTagViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tagsByType = await _context.TAG
                .GroupBy(t => t.typeTag)
                .Select(g => new TagGroup
                {
                    TypeTag = g.Key,
                    Tags = g.ToList()
                })
                .ToListAsync();

            return View("FilterTag", tagsByType);
        }
    }
}