using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
    [Route("Admin/Card")]
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        // GET: Admin/Card
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Card(string search)
        {
            search ??= ""; // Xử lý nếu search null
            ViewData["CurrentSearch"] = search;

            var cards = await _cardService.SearchCard(search);
            return View(cards);
        }

        // GET: Admin/Card/Create
        [Route("Create")]
        public IActionResult Create()
        {
            var newCard = new Card
            {
                idCard = _cardService.GenerateCardID(),
                dateCard = DateOnly.FromDateTime(DateTime.Now)
            };
            return View(newCard);
        }

        // POST: Admin/Card/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Card card)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cardService.AddCard(card);
                    TempData["Success"] = "Thêm thẻ thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi thêm thẻ (Check ID hoặc SQL): " + ex.Message);
                }
            }
            return View(card);
        }

        // GET: Admin/Card/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null) return NotFound();

            return View(card);
        }

        // POST: Admin/Card/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Card card)
        {
            if (id != card.idCard) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _cardService.EditCard(card);
                    TempData["Success"] = "Cập nhật thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                }
            }
            return View(card);
        }

        // GET: Admin/Card/Delete/5
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null) return NotFound();

            return View(card);
        }

        // POST: Admin/Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _cardService.DeleteCard(id);
                TempData["Success"] = "Xóa thẻ thành công";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi xóa thẻ: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}