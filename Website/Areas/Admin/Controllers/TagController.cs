using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;
using System.Threading.Tasks;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Tag")]
    public class TagController : Controller
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Tag()
        {
            var tags = await _tagService.GetTagsAsync();
            return View(tags);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagService.AddTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, Tag tag)
        {
            if (id != tag.idTag) return NotFound();
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id) // id là int
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null) return NotFound();
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tagService.DeleteTag(id);
            return RedirectToAction(nameof(Index));
        }
    }
}