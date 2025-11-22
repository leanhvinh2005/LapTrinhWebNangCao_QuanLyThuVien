using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public int idTag { get; set; }

        [Required(ErrorMessage = "Không được để trống tên")]
        [StringLength(30, ErrorMessage = "Số ký tự không vượt quá 30")]
        public string nameTag { get; set; }

        [Required(ErrorMessage = "Không được để trống loại")]
        [StringLength(30, ErrorMessage = "Số ký tự không vượt quá 30")]
        public string typeTag { get; set; }
    }
}
