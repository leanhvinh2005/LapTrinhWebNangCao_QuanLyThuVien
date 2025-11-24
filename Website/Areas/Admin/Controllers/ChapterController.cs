using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;
using System.Threading.Tasks;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Chapter")]
    public class ChapterController : Controller
    {
        private readonly ChapterService _chapterService;

        public ChapterController(ChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        // GET: /Admin/Chapter
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(string search)
        {
            search ??= "";
            var chapters = await _chapterService.SearchChapter(search);
            ViewData["CurrentSearch"] = search;
            return View(chapters);
        }

        // GET: /Admin/Chapter/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Chapter/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Chapter chapter)
        {
            // Kiểm tra ID đã tồn tại chưa (vì ID này nhập tay)
            var existingChapter = await _chapterService.GetChapterById(chapter.idChapter);
            if (existingChapter != null)
            {
                ModelState.AddModelError("idChapter", "ID Chương này đã tồn tại.");
            }

            if (ModelState.IsValid)
            {
                await _chapterService.AddChapter(chapter);
                return RedirectToAction(nameof(Index));
            }
            return View(chapter);
        }

        // GET: /Admin/Chapter/Edit/CL001
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var chapter = await _chapterService.GetChapterById(id);
            if (chapter == null) return NotFound();

            return View(chapter);
        }

        // POST: /Admin/Chapter/Edit/CL001
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Chapter chapter)
        {
            if (id != chapter.idChapter) return NotFound();

            if (ModelState.IsValid)
            {
                await _chapterService.EditChapter(chapter);
                return RedirectToAction(nameof(Index));
            }
            return View(chapter);
        }

        // GET: /Admin/Chapter/Delete/CL001
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var chapter = await _chapterService.GetChapterById(id);
            if (chapter == null) return NotFound();

            return View(chapter);
        }

        // POST: /Admin/Chapter/Delete/CL001
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _chapterService.DeleteChapter(id);
            return RedirectToAction(nameof(Index));
        }
    }
}