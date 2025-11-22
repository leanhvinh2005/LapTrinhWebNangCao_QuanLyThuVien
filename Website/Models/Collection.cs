using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Collection
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống ID")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "ID sưu tập không hợp lệ (VD: CL01 || AT01)")]
        [RegularExpression(@"^[A-Za-z]{2}.*[0-9]{2}$", ErrorMessage = "ID sưu tập không hơp lệ (VD: CL01 || AT01)")]
        public string idCollection { get; set; }

        [Required(ErrorMessage = "Không được để trống tên")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string nameCollection { get; set; }

        [Required(ErrorMessage = "Không được để trống mô tả")]
        public string descriptionCollection { get; set; }

        [Required(ErrorMessage = "Không được để trống path hình ảnh")]
        [StringLength(200, ErrorMessage = "Số ký tự không vượt quá 200")]
        public string imageCollection { get; set; }


        [NotMapped]
        public List<Book> books { get; set; } = new();
    }
}
