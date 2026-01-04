using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models.ViewModels;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            // 1. Lấy số liệu thống kê
            var totalBooks = await _context.SACH.CountAsync();
            var totalReaders = await _context.ACCOUNT_USER.CountAsync();

            // Đếm số phiếu đang trạng thái "Dang muon"
            var borrowingCount = await _context.MUONTRA.CountAsync(b => b.statusBorrow == "Dang muon");

            // Đếm số phiếu "Qua han"
            var overdueCount = await _context.MUONTRA.CountAsync(b => b.statusBorrow == "Qua han");

            // 2. Lấy 5 phiếu mượn gần nhất để hiển thị
            var recentBorrows = await _context.MUONTRA
                .OrderByDescending(b => b.dateBorrow)
                .Take(5)
                .ToListAsync();
            var viewModel = new DashboardViewModel
            {
                TotalBooks = totalBooks,
                TotalReaders = totalReaders,
                BorrowingCount = borrowingCount,
                OverdueCount = overdueCount,
                RecentBorrows = recentBorrows,

                // Dữ liệu giả lập cho biểu đồ
                ChartLabels = new List<string> { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "CN" },
                ChartData = new List<int> { 12, 19, 3, 5, 2, 3, 10 }
            };

            return View(viewModel);
        }
    }
}