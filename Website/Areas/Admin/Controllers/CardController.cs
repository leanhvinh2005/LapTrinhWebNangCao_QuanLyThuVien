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
        private readonly UserService _userService;     
        private readonly MemberService _memberService; 

        
        public CardController(
            CardService cardService,
            UserService userService,
            MemberService memberService)
        {
            _cardService = cardService;
            _userService = userService;
            _memberService = memberService;
        }

        // GET: Admin/Card
        
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Card(string search)
        {
            search ??= "";
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
                dateCard = DateOnly.FromDateTime(DateTime.Now),
                statusCard = "CREATED"
            };
            return View(newCard);
        }

        // POST: Admin/Card/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Card card)
        {
            if (string.IsNullOrEmpty(card.statusCard)) card.statusCard = "CREATED";
            if (card.dateCard == default) card.dateCard = DateOnly.FromDateTime(DateTime.Now);
            if (string.IsNullOrEmpty(card.idCard)) card.idCard = _cardService.GenerateCardID();

            ModelState.Remove(nameof(card.idCard));
            ModelState.Remove(nameof(card.statusCard));
            ModelState.Remove(nameof(card.dateCard));

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
                    ModelState.AddModelError("", "Lỗi khi thêm thẻ: " + ex.Message);
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
            ModelState.Remove(nameof(card.idCard));

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
                // BƯỚC 1: Tìm xem thẻ này có đang liên kết với User nào không?
                var members = await _memberService.SearchMember(id);
                var linkedMember = members.FirstOrDefault(m => m.idCard == id);
                // BƯỚC 2: Nếu tìm thấy -> Xóa User trước
                if (linkedMember != null)
                {
                    await _userService.DeleteUser(linkedMember.idUser);
                }
                // BƯỚC 3: Xóa thẻ (Dù có User hay không thì cuối cùng cũng xóa thẻ)
                await _cardService.DeleteCard(id);

                TempData["Success"] = "Đã xóa thẻ và tài khoản liên kết (nếu có).";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi xóa dữ liệu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}