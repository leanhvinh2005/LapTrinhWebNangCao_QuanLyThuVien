using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Services;
using System.Threading.Tasks;

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

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Card(string search)
        {
            search ??= "";
            var cards = await _cardService.SearchCard(search);
            ViewData["CurrentSearch"] = search;
            return View(cards);
        }

        // GET: /Admin/Card/Create
        [Route("Create")]
        public IActionResult Create()
        {
            // ĐIỂM "ĂN TIỀN" LÀ Ở ĐÂY
            var newCard = new Card
            {
                // Tự động tạo ID
                idCard = _cardService.GenerateCardID(),
                // Tự động điền ngày hôm nay
                dateCard = DateOnly.FromDateTime(DateTime.Now)
            };

            // Gửi model với ID và Ngày đã điền sẵn sang View
            return View(newCard);
        }

        // POST: /Admin/Card/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Card card)
        {
            if (ModelState.IsValid)
            {
                // SP AddCard của bạn không có @status, nên nó sẽ bị bỏ qua
                await _cardService.AddCard(card);
                return RedirectToAction(nameof(Index));
            }
            // Nếu lỗi, trả về view với dữ liệu (và ID đã tạo)
            return View(card);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id) // id là string
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null) return NotFound();
            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, Card card)
        {
            if (id != card.idCard) return NotFound();
            if (ModelState.IsValid)
            {
                await _cardService.EditCard(card);
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null) return NotFound();
            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _cardService.DeleteCard(id);
            return RedirectToAction(nameof(Index));
        }
    }
}