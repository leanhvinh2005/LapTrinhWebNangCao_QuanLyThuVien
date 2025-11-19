using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website.Data;
using Website.Models;
using Website.Services;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/Borrow")]
    public class BorrowController : Controller
    {
        private readonly BorrowService _borrowService;
        private readonly ApplicationDbContext _context;

        public BorrowController(BorrowService borrowService, ApplicationDbContext context)
        {
            _borrowService = borrowService;
            _context = context;
        }

        // GET: Admin/Borrow
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var borrows = await _borrowService.GetAllBorrowsAsync();
            return View(borrows);
        }

        // GET: Admin/Borrow/Create
        [Route("Create")]
        public IActionResult Create()
        {
           
            ViewBag.CardList = new SelectList(_context.THETHUVIEN, "idCard", "idCard");

            

            var model = new Borrow
            {
                dateBorrow = DateOnly.FromDateTime(DateTime.Now),
                statusBorrow = "Dang muon"
            };
            return View(model);
        }

        // POST: Admin/Borrow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int newId = await _borrowService.AddBorrow(borrow);
                    TempData["Success"] = $"Tạo phiếu mượn #{newId} thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                }
            }

            
            ViewBag.CardList = new SelectList(_context.THETHUVIEN, "idCard", "idCard", borrow.idCard);

            
            return View(borrow);
        }

        // GET: Admin/Borrow/Edit/{id}
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var borrow = await _borrowService.GetBorrowByIdAsync(id);
            if (borrow == null) return NotFound();

            ViewBag.CardList = new SelectList(_context.THETHUVIEN, "idCard", "idCard", borrow.idCard);

            return View(borrow);
        }

        // POST: Admin/Borrow/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Borrow borrow)
        {
            if (id != borrow.idBorrow) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _borrowService.EditBorrow(borrow);
                    TempData["Success"] = "Cập nhật phiếu mượn thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi cập nhật: " + ex.Message);
                }
            }

            ViewBag.CardList = new SelectList(_context.THETHUVIEN, "idCard", "idCard", borrow.idCard);
            return View(borrow);
        }

        // GET: Admin/Borrow/Delete/{id}
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var borrow = await _borrowService.GetBorrowByIdAsync(id);
            if (borrow == null) return NotFound();
            return View(borrow);
        }

        // POST: Admin/Borrow/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _borrowService.DeleteBorrow(id);
                TempData["Success"] = "Đã xóa phiếu mượn.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể xóa: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // Action thêm sách
        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddBook(int idBorrow, string idBook)
        {
            try { await _borrowService.AddBookToBorrow(idBorrow, idBook); TempData["Success"] = "Thêm sách thành công."; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction("Edit", new { id = idBorrow });
        }

        // Action xóa sách
        [HttpPost]
        [Route("RemoveBook")]
        public async Task<IActionResult> RemoveBook(int idBorrow, string idBook)
        {
            try { await _borrowService.RemoveBookFromBorrow(idBorrow, idBook); TempData["Success"] = "Đã xóa sách khỏi phiếu."; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction("Edit", new { id = idBorrow });
        }
    }
}