using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Chapter
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống ID")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "ID trang không hợp lệ (VD: CL001)")]
        [RegularExpression(@"^[A-Za-z]{2}.*[0-9]{3}$", ErrorMessage = "ID sách không hơp lệ (VD: CL001)")]
        public string idChapter { get; set; }

        [Required(ErrorMessage = "Không được để trống số trang")]
        public int numberChapter { get; set; }

        [Required(ErrorMessage = "Không được để trống tựa đề")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string titleChapter { get; set; }

        [Required(ErrorMessage = "Không được để trống trang")]
        public string contentChapter { get; set; }


    }
}
