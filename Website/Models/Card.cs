using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class Card
    {
        [Key]
        [Required(ErrorMessage = "Không được để trống ID")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "ID thẻ không hợp lệ (VD: CA04B8AU91P)")]
        public string idCard { get; set; }

        [Required(ErrorMessage = "Không được để trống họ tên")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string nameCard { get; set; }

        [Required(ErrorMessage = "Không được để trống email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string emailCard { get; set; }

        [Required(ErrorMessage = "Không được để trống địa chỉ")]
        [StringLength(200, ErrorMessage = "Số ký tự không vượt quá 200")]
        public string addressCard { get; set; }

        [Required(ErrorMessage = "Không được để trống số điện thoại")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string phoneCard { get; set; }

        [Required(ErrorMessage = "Không được để trống ngày sinh")]
        [DataType(DataType.Date)]
        [BirthDate(16)]
        public DateOnly dateCard { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly startCard { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly expireCard { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string statusCard { get; set; }


        [NotMapped]
        public List<Borrow> borrows { get; set; } = new();
    }
}
