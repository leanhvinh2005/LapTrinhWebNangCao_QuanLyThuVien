using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Author")]
    public class AuthorController : Controller
    {

        private readonly CollectionService _collectionService;

        public AuthorController(CollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        // GET: /Admin/Author
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Author(string search)
        {
            search ??= "";
            var allItems = await _collectionService.SearchCollection(search);


            var authors = allItems.Where(c => c.idCollection.StartsWith("AU")).ToList();

            ViewData["CurrentSearch"] = search;

            return View(authors);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Collection collection)
        {

            if (!collection.idCollection.StartsWith("AU"))
            {
                ModelState.AddModelError("idCollection", "ID Tác giả phải bắt đầu bằng 'AU'.");
            }

            var existing = await _collectionService.GetCollectionByIdAsync(collection.idCollection);
            if (existing != null) ModelState.AddModelError("idCollection", "ID này đã tồn tại.");

            if (ModelState.IsValid)
            {
                await _collectionService.AddCollection(collection);
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {

            if (!id.StartsWith("AU")) return NotFound();

            var collection = await _collectionService.GetCollectionByIdAsync(id);
            if (collection == null) return NotFound();
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Collection collection)
        {
            if (id != collection.idCollection) return NotFound();
            if (!collection.idCollection.StartsWith("AU")) return BadRequest("Sai định dạng ID");

            if (ModelState.IsValid)
            {
                await _collectionService.EditCollection(collection);
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!id.StartsWith("AU")) return NotFound();
            var collection = await _collectionService.GetCollectionByIdAsync(id);
            if (collection == null) return NotFound();
            return View(collection);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!id.StartsWith("AU")) return BadRequest();
            await _collectionService.DeleteCollection(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
