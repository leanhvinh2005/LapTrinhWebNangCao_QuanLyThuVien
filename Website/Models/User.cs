using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class User
    {
        [Key]
        [Required]
        public int idUser { get; set; }

        [Required(ErrorMessage = "Không được để trống username")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string nameUser { get; set; }

        [Required(ErrorMessage = "Không được để trống email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string emailUser { get; set; }

        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Số ký tự không vượt quá 100")]
        public string passwordUser { get; set; }


        [NotMapped]
        public Member? member { get; set; }

        [NotMapped]
        public Librarian? librarian { get; set; }
    }
}
