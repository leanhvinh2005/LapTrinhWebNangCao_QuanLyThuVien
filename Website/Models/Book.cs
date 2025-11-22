using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống ID")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "ID sách không hợp lệ (VD: CL01)")]
        [RegularExpression(@"^[A-Za-z]{2}.*[0-9]{2}$", ErrorMessage = "ID sách không hơp lệ (VD: CL01)")]
        public string idBook { get; set; }

        [Required(ErrorMessage = "Không được để trống tên sách")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string nameBook { get; set; }

        [Required(ErrorMessage = "Không được để trống mô tả sách")]
        public string descriptionBook { get; set; }

        [Required(ErrorMessage = "Không được để trống tác giả")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string authorBook { get; set; }

        [Required(ErrorMessage = "Không được để trống nhà xuất bản")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string publisherBook { get; set; }

        [Required(ErrorMessage = "Không được để trống ngày xuất bản")]
        [DataType(DataType.Date)]
        [DateFuture]
        public DateOnly dateBook { get; set; }

        [Required(ErrorMessage = "Không được để trống format")]
        [StringLength(20, ErrorMessage = "Số ký tự không vượt quá 20")]
        public string formatBook { get; set; }

        [StringLength(200, ErrorMessage = "Số ký tự không vượt quá 200")]
        public string? noteBook { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string statusBook { get; set; }

        [Required(ErrorMessage = "Không được để trống path hình ảnh")]
        [StringLength(200, ErrorMessage = "Số ký tự không vượt quá 200")]
        public string imageBook { get; set; }


        [NotMapped]
        public List<Tag> tags { get; set; } = new();

        [NotMapped]
        public List<Chapter> chapters { get; set; } = new();

        [NotMapped]
        public bool IsSelected { get; set; }

    }
}
