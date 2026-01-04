using System.Collections.Generic;

namespace Website.Models.ViewModels
{
    public class DashboardViewModel
    {
        
        public int TotalBooks { get; set; }        // Tổng số sách
        public int TotalReaders { get; set; }      // Tổng độc giả/User
        public int BorrowingCount { get; set; }    // Số phiếu đang mượn
        public int OverdueCount { get; set; }      // Số phiếu quá hạn (Quan trọng!)

        // Dữ liệu cho biểu đồ 
        public List<string> ChartLabels { get; set; } // Ngày (Thứ 2, Thứ 3...)
        public List<int> ChartData { get; set; }      // Số lượng mượn

        // Danh sách các phiếu mượn mới nhất hoặc quá hạn cần xử lý ngay
        public List<Borrow> RecentBorrows { get; set; }
    }
}