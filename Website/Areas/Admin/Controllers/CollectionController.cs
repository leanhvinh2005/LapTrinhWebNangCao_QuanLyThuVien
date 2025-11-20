using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;
using System.Threading.Tasks;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Collection")]
    public class CollectionController : Controller
    {
        private readonly CollectionService _collectionService;

        public CollectionController(CollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Collection(string search)
        {
            search ??= "";
            var collections = await _collectionService.SearchCollection(search);
            ViewData["CurrentSearch"] = search;
            return View(collections);
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
            var collection = await _collectionService.GetCollectionByIdAsync(id);
            if (collection == null) return NotFound();
            return View(collection);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _collectionService.DeleteCollection(id);
            return RedirectToAction(nameof(Index));
        }
    }
}