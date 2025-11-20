using System;
using System.ComponentModel.DataAnnotations;

namespace Website.Models.ViewModels
{
    public class UserCreationViewModel
    {
        // --- Thông tin chung (Tài khoản) ---
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ tên")]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; } // Giá trị: "Member" hoặc "Librarian"

        // --- Dành cho Độc giả (Không bắt buộc ở đây, sẽ check trong Controller) ---
        public string? LibraryCardId { get; set; }

        // --- Dành cho Thủ thư ---
        public string? JobTitle { get; set; }
        public DateTime? HireDate { get; set; }
    }
}